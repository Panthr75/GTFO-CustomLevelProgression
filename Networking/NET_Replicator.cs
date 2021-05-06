using System.Collections.Generic;

namespace CustomLevelProgression.Networking
{
    public abstract class NET_Replicator
    {
        public abstract ushort Key { get; }
        public abstract bool ShouldHandlePacket(UnhollowerBaseLib.Il2CppStructArray<byte> bytes, ref int offset);
        public abstract void OnReceiveBytes(UnhollowerBaseLib.Il2CppStructArray<byte> bytes, int offset = 0);

        private static Dictionary<ushort, NET_Replicator> replicators = new Dictionary<ushort, NET_Replicator>();

        public static bool TryGetReplicator(ushort key, out NET_Replicator replicator) => replicators.TryGetValue(key, out replicator);

        public static bool AddReplicator(NET_Replicator replicator)
        {
            if (replicator != null && !replicators.ContainsKey(replicator.Key))
            {
                replicators.Add(replicator.Key, replicator);
                return true;
            }
            return false;
        }

        public static bool RemoveReplicator(NET_Replicator replicator)
        {
            if (replicator == null)
                return false;

            return replicators.Remove(replicator.Key);
        }
    }
}