using CustomLevelProgression.Utilities;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CustomLevelProgression.Networking
{
    public static class NET
    {
        public static bool IsMaster => SNetwork.SNet.IsMaster;
        public static SNetwork.SNet_Player SNetMaster => SNetwork.SNet.Master;
        private const int JUNK_SIZE = 5;

        public static bool ReceiveBytes(UnhollowerBaseLib.Il2CppStructArray<byte> bytes)
        {
            if (bytes.Length > JUNK_SIZE + sizeof(ushort))
            {
                for (int index = 0; index < JUNK_SIZE; index++)
                {
                    if (bytes[index] != 255)
                        return false;
                }

                int offset = JUNK_SIZE;
                ushort replicatorID = Il2CppSystem.BitConverter.ToUInt16(bytes, offset);

                if (NET_Replicator.TryGetReplicator(replicatorID, out NET_Replicator replicator) && replicator.ShouldHandlePacket(bytes, ref offset))
                {
                    try
                    {
                        Log.Message("Received custom packet");
                        replicator.OnReceiveBytes(bytes, offset);
                    }
                    catch(Exception exception)
                    {
                        Debug.LogError("An exception occurred whilst handling a custom NET_Replicator: " + exception);
                    }
                    return true;
                }
            }

            return false;
        }

        public static byte[] GetBytesFromPacket<T>(T packet, ExtendedPlayerAgent owner, byte packetID) where T : struct
        {
            int size = Marshal.SizeOf(packet);
            ushort ownerRepID = owner.m_replicator.Key;
            var idBytes = BitConverter.GetBytes(ownerRepID);
            int offset = JUNK_SIZE + idBytes.Length;
            byte[] packetBytes = new byte[size + offset + 1];

            for (int index = 0; index < JUNK_SIZE; index++)
            {
                packetBytes[index] = 255;
            }

            for (int packetIndex = JUNK_SIZE, index = 0; index < idBytes.Length; index++, packetIndex++)
            {
                packetBytes[packetIndex] = idBytes[index];
            }

            packetBytes[offset] = packetID;

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, packetBytes, offset + 1, size);
            Marshal.FreeHGlobal(ptr);

            return packetBytes;
        }

        public static void Send(byte[] bytes, SNetwork.SNet_ChannelType type)
        {
            SNetwork.SNet.GetSendSettings(ref type, out SNetwork.SNet_SendGroup group, out SNetwork.SNet_SendQuality quality, out int channel);
            SNetwork.SNet.Core.SendBytes(bytes, group, quality, channel);
        }

        public static void Send(byte[] bytes, SNetwork.SNet_ChannelType type, SNetwork.SNet_Player player)
        {
            SNetwork.SNet.GetSendSettings(ref type, out SNetwork.SNet_SendGroup _, out SNetwork.SNet_SendQuality quality, out int channel);
            SNetwork.SNet.Core.SendBytes(bytes, quality, channel, player);
        }
    }
}
