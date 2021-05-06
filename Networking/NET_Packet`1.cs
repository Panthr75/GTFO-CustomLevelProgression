namespace CustomLevelProgression.Networking
{
    public class NET_Packet<T> : NET_Packet where T : struct
    {
        private T m_data = new T();
    }
}
