using Player;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CustomLevelProgression.Networking.Progression
{
    public class NET_RequestProgressionDataPacket : NET_Packet
    {
        public NET_ProgressionReplicator Replicator { get; set; }

        public NET_RequestProgressionDataPacket(NET_ProgressionReplicator replicator) : base(2)
        {
            Replicator = replicator;
        }

        public override void HandleBytes(byte[] bytes, int offset)
        {
            pProgressionDataRequest requestData = new pProgressionDataRequest();
            int size = Marshal.SizeOf(requestData);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, offset, ptr, size);
            requestData = (pProgressionDataRequest)Marshal.PtrToStructure(ptr, requestData.GetType());
            Debug.LogWarning("Received progression request (ACK: " + requestData.ack + ")");
            OnReceive(requestData);
            Marshal.FreeHGlobal(ptr);
        }

        public void OnReceive(pProgressionDataRequest requestData)
        {
            if (requestData.from.TryGet(out Player.PlayerAgent p) && ExtendedPlayerAgent.TryGetOrCreate(p, out ExtendedPlayerAgent player))
            {
                player.ProgressionSynced = requestData.ack;
                if (NET.IsMaster && !player.ProgressionSynced)
                {
                    if (ExtendedPlayerAgent.TryGet(NET.SNetMaster.PlayerAgent.TryCast<PlayerAgent>(), out ExtendedPlayerAgent master))
                    {
                        master.ProgressionReplicator.SendProgressionData(player);
                    }
                    else
                    {
                        Debug.LogError("COULD NOT FIND THE MASTA!!!!");
                    }
                }
            }
            else
            {
                Debug.Log("Failed to extract player from progression data (p is null: " + (p == null) + ")");
            }
        }

        public void SendRequest()
        {
            Debug.Log("Requesting progression data as " + (NET.IsMaster ? "master" : "client"));

            pProgressionDataRequest data = new pProgressionDataRequest();
            data.ack = false;
            data.from.Set(Replicator.Owner.WrappedObj);

            byte[] bytes = NET.GetBytesFromPacket(data, Replicator.Key, ID);
            NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, NET.SNetMaster);
        }

        public void SendAck()
        {
            Debug.Log("ACK progression data as " + (NET.IsMaster ? "master" : "client"));

            pProgressionDataRequest data = new pProgressionDataRequest();
            data.ack = true;
            data.from.Set(Replicator.Owner.WrappedObj);

            byte[] bytes = NET.GetBytesFromPacket(data, Replicator.Key, ID);
            NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, NET.SNetMaster);
        }
    }
}