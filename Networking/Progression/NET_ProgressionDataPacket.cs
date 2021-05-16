using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CustomLevelProgression.Networking.Progression
{
    public class NET_ProgressionDataPacket : NET_Packet
    {
        public NET_ProgressionReplicator Replicator { get; set; }

        public NET_ProgressionDataPacket(NET_ProgressionReplicator replicator) : base(1)
        {
            Replicator = replicator;
        }

        public override void HandleBytes(byte[] bytes, int offset)
        {
            pProgressionData progressionData = new pProgressionData();
            var size = Marshal.SizeOf(progressionData);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, offset, ptr, size);
            progressionData = (pProgressionData)Marshal.PtrToStructure(ptr, progressionData.GetType());
            Debug.LogWarning("Received progression data");
            OnReceive(progressionData);
            Marshal.FreeHGlobal(ptr);
        }

        public void OnReceive(pProgressionData progressionData)
        {
            Debug.Log("RECEIVED PROGRESSION DATA!");
            Replicator.Owner.SessionProgressionData = progressionData.Create();

            Replicator.AckProgressionData();
        }

        public void Send(ExtendedPlayerAgent player)
        {
            if (NET.IsMaster)
            {
                if (player == null || player.WrappedObj == null)
                {
                    Debug.LogError("Tried to send progression data to a null player.");
                    return;
                }

                pProgressionData data = Replicator.Owner.ProgressionData.ToPacket();
                data.to.Set(player.WrappedObj);

                byte[] bytes = NET.GetBytesFromPacket(data, Replicator.Key, ID);
                NET.Send(bytes, SNetwork.SNet_ChannelType.GameReceiveCritical, player.Owner);
            }
            else
            {
                Debug.LogError("Attempting to send progression data when not the master.");
            }
        }
    }
}
