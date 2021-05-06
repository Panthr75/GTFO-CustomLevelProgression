namespace CustomLevelProgression.Networking
{
    public abstract class NET_Packet
    {
        public uint PacketID { get; set; }

        public virtual void Setup(uint packetID)
        {
            this.PacketID = packetID;
        }

        public virtual void ProcessBytes(byte[] bytes)
        {
        }
    }
}
