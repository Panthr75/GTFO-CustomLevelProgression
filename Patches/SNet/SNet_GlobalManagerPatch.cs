using HarmonyLib;
using Player;
using SNetwork;

namespace CustomLevelProgression.Patches.SNet
{
    public class SNet_GlobalManagerPatch : CustomPatch
    {
        public SNet_GlobalManagerPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(SNet_GlobalManager), nameof(SNet_GlobalManager.OnPlayerEvent))
        { }

        public static void Invoke(SNet_Player player, SNet_PlayerEvent playerEvent)
        {
            if (playerEvent == SNet_PlayerEvent.PlayerAgentSpawned)
            {
                if (ExtendedPlayerAgent.TryGetOrCreate(player.PlayerAgent.TryCast<PlayerAgent>(), out ExtendedPlayerAgent p))
                {
                    p.InternalSetup();

                    if (p.IsLocallyOwned)
                    {
                        p.ProgressionReplicator.RequestProgressionData();
                    }
                }
            }
            else if (playerEvent == SNet_PlayerEvent.PlayerAgentDeSpawned)
            {
                if (ExtendedPlayerAgent.TryGet(player.PlayerAgent.TryCast<PlayerAgent>(), out ExtendedPlayerAgent p))
                {
                    ExtendedPlayerAgent.Remove(p.WrappedObj);
                    p.InternalOnDestroyed();
                }
            }
        }
    }
}
