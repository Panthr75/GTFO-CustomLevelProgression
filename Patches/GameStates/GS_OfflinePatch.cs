using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_OfflinePatch : CustomPatch
    {
        public GS_OfflinePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_Offline), nameof(GS_Offline.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.Offline;
        }
    }
}
