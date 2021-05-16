using Agents;
using CustomLevelProgression.Networking;
using CustomLevelProgression.Networking.Progression;
using CustomLevelProgression.Wrappers.WPlayer;
using Player;
using System.Collections.Generic;

namespace CustomLevelProgression
{
    public class ExtendedPlayerAgent : PlayerAgentWrapper
    {
        private static Dictionary<ushort, ExtendedPlayerAgent> wrapped = new Dictionary<ushort, ExtendedPlayerAgent>();

        public NET_ProgressionReplicator ProgressionReplicator { get; set; }
        public ProgressionData ProgressionData { get; set; }
        public ProgressionData SessionProgressionData { get; set; }
        public bool ProgressionSynced { get; set; }

        public static ExtendedPlayerAgent LocalPlayer
        {
            get
            {
                TryGetOrCreate(PlayerManager.GetLocalPlayerAgent(), out ExtendedPlayerAgent localPlayer);
                return localPlayer;
            }
        }

        private ExtendedPlayerAgent(PlayerAgent player) : base(player)
        {
        }

        internal void InternalSetup()
        {
            if (ProgressionReplicator != null)
                NET_Replicator.RemoveReplicator(ProgressionReplicator);

            ProgressionReplicator = new NET_ProgressionReplicator(this);
            NET_Replicator.AddReplicator(ProgressionReplicator);

            if (IsLocallyOwned)
            {
                ProgressionData = ProgressionData.LoadFromFile();
            }
        }

        internal void InternalOnDestroyed()
        {
            NET_Replicator.RemoveReplicator(ProgressionReplicator);
            Remove(this.WrappedObj);
        }

        public static void Remove(PlayerAgent player)
        {
            if (player != null && wrapped.ContainsKey(player.GlobalID))
            {
                wrapped.Remove(player.GlobalID);
            }
        }

        public static bool TryGetOrCreate(PlayerAgent player, out ExtendedPlayerAgent extendedPlayer)
        {
            if (!TryGet(player, out extendedPlayer))
            {
                if (player == null)
                {
                    return false;
                }

                extendedPlayer = new ExtendedPlayerAgent(player);
                wrapped.Add(player.GlobalID, extendedPlayer);
            }

            return true;
        }

        public static bool TryGetOrCreate(Agent agent, out ExtendedPlayerAgent extendedPlayer)
        {
            if (!TryGet(agent, out extendedPlayer))
            {
                var player = agent?.TryCast<PlayerAgent>();
                if (player == null)
                {
                    return false;
                }

                extendedPlayer = new ExtendedPlayerAgent(player);
                wrapped.Add(agent.GlobalID, extendedPlayer);
            }

            return true;
        }

        public static bool TryGet(PlayerAgent player, out ExtendedPlayerAgent extendedPlayer)
        {
            if (player == null)
            {
                extendedPlayer = null;
                return false;
            }

            return wrapped.TryGetValue(player.GlobalID, out extendedPlayer);
        }

        public static bool TryGet(Agent agent, out ExtendedPlayerAgent extendedPlayer)
        {
            if (agent == null)
            {
                extendedPlayer = null;
                return false;
            }

            return wrapped.TryGetValue(agent.GlobalID, out extendedPlayer);
        }
    }
}
