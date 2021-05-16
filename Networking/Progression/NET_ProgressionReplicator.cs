using Player;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CustomLevelProgression.Networking.Progression
{
    public class NET_ProgressionReplicator : NET_Replicator
    {
        public List<NET_Packet> packets;

        public ExtendedPlayerAgent Owner { get; }
        public override ushort Key => Owner?.m_replicator?.Key ?? 0;
        

        public NET_ProgressionReplicator(ExtendedPlayerAgent owner)
        {
            this.Owner = owner;
            packets = new List<NET_Packet>();

            this.AddPacket(new NET_RequestProgressionDataPacket(this));
            this.AddPacket(new NET_ProgressionDataPacket(this));
        }

        public bool ContainsPacket<T>() where T : NET_Packet
        {
            foreach (var packet in packets)
            {
                if (packet is T)
                    return true;
            }
            return false;
        }

        public bool TryGetPacket<T>(out T packet) where T : NET_Packet
        {
            foreach (var pack in packets)
            {
                if (pack is T p)
                {
                    packet = p;
                    return true;
                }
            }
            packet = null;
            return false;
        }

        public void AddPacket(NET_Packet packet)
        {
            this.packets.Add(packet);
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

            foreach (var packet in packets)
            {
                if (packet.ID == id)
                {
                    packet.HandleBytes(bytes, offset);
                    break;
                }
            }
        }

        public void RequestProgressionData()
        {
            foreach (var packet in packets)
            {
                if (packet is NET_RequestProgressionDataPacket requestPacket)
                {
                    requestPacket.SendRequest();
                    break;
                }
            }
        }

        public void AckProgressionData()
        {
            foreach (var packet in packets)
            {
                if (packet is NET_RequestProgressionDataPacket requestPacket)
                {
                    requestPacket.SendAck();
                    break;
                }
            }
        }

        public void SendProgressionData(ExtendedPlayerAgent player)
        {
            foreach (var packet in packets)
            {
                if (packet is NET_ProgressionDataPacket progressionDataPacket)
                {
                    progressionDataPacket.Send(player);
                    break;
                }
            }
        }
    }
}