namespace CustomLevelProgression.Networking
{
    public abstract class NET_Packet
    {
        public byte ID { get; set; }

        public NET_Packet(byte id)
        {
            this.ID = id;
        }

        public abstract void HandleBytes(byte[] bytes, int offset);
    }
}