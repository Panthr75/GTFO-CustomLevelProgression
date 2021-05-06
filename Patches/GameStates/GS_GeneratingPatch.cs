using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_GeneratingPatch : CustomPatch
    {
        public GS_GeneratingPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_Generating), nameof(GS_Generating.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.Generating;
        }
    }
}
