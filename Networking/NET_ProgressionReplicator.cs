using Player;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CustomLevelProgression.Networking
{
    public class NET_ProgressionReplicator : NET_Replicator
    {
        public const byte PROGRESSION_ID = 1;
        public const byte REQUEST_ID = 2;
        public const byte LEVEL_STATE_ID = 3;
        public const byte EVENT_SEQUENCE_ID = 4;

        public ExtendedPlayerAgent Owner { get;  }
        public override ushort Key => Owner?.m_replicator?.Key ?? 0;

        public NET_ProgressionReplicator(ExtendedPlayerAgent owner)
        {
            this.Owner = owner;
        }

        public override bool ShouldHandlePacket(UnhollowerBaseLib.Il2CppStructArray<byte> bytes, ref int offset)
        {
            if (bytes.Length < sizeof(ushort) + offset)
                return false;

            ushort replicatorID = BitConverter.ToUInt16(bytes, offset);
            if (replicatorID != Owner.m_replicator.Key)
                return false;

            offset += sizeof(ushort);
            return true;
        }

        public override void OnReceiveBytes(UnhollowerBaseLib.Il2CppStructArray<byte> bytes, int offset = 0)
        {
            OnReceiveBytes((byte[])bytes, offset);
        }

        public unsafe void OnReceiveBytes(byte[] bytes, int offset = 0)
        {
            byte id = bytes[offset];
            offset++;

            IntPtr ptr;
            int size;
            switch (id)
            {
                case PROGRESSION_ID:
                    pProgressionData progressionData = new pProgressionData();
                    size = Marshal.SizeOf(progressionData);
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(bytes, offset, ptr, size);
                    progressionData = (pProgressionData)Marshal.PtrToStructure(ptr, progressionData.GetType());
                    Debug.LogWarning("Received progression data");
                    OnReceiveProgressionData(progressionData);
                    Marshal.FreeHGlobal(ptr);
                    break;
                case REQUEST_ID:
                    pProgressionDataRequest requestData = new pProgressionDataRequest();
                    size = Marshal.SizeOf(requestData);
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(bytes, offset, ptr, size);
                    requestData = (pProgressionDataRequest)Marshal.PtrToStructure(ptr, requestData.GetType());
                    Debug.LogWarning("Received progression request (ACK: " + requestData.ack + ")");
                    OnReceiveProgressionRequest(requestData);
                    Marshal.FreeHGlobal(ptr);
                    break;

                default:
                    Debug.LogWarning("Failed to get the id");
                    break;

            }
        }

        public void RequestProgressionData()
        {
            Debug.Log("Requesting progression data as " + (NET.IsMaster ? "master" : "client"));

            pProgressionDataRequest data = new pProgressionDataRequest();
            data.ack = false;
            data.from.Set(Owner.WrappedObj);

            byte[] bytes = NET.GetBytesFromPacket(data, Owner, REQUEST_ID);
            NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, NET.SNetMaster);
        }

        public void AckProgressionData()
        {
            Debug.Log("ACK progression data as " + (NET.IsMaster ? "master" : "client"));

            pProgressionDataRequest data = new pProgressionDataRequest();
            data.ack = true;
            data.from.Set(Owner.WrappedObj);

            byte[] bytes = NET.GetBytesFromPacket(data, Owner, REQUEST_ID);
            NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, NET.SNetMaster);
        }

        public void SendProgressionData(ExtendedPlayerAgent player)
        {
            if (NET.IsMaster)
            {
                if (player == null || player.WrappedObj == null)
                {
                    Debug.LogError("Tried to send progression data to a null player.");
                    return;
                }

                pProgressionData data = Owner.ProgressionData.ToPacket();
                data.to.Set(player.WrappedObj);

                byte[] bytes = NET.GetBytesFromPacket(data, Owner, PROGRESSION_ID);
                NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, player.Owner);
            }
            else
            {
                Debug.LogError("Attempting to send progression data when not the master.");
            }
        }

        public void OnReceiveProgressionRequest(pProgressionDataRequest requestData)
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

        public void OnReceiveProgressionData(pProgressionData progressionData)
        {
            Debug.Log("RECEIVED PROGRESSION DATA!");
            Owner.SessionProgressionData = progressionData.Create();

            AckProgressionData();
        }
    }
}