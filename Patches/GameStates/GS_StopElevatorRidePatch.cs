using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_StopElevatorRidePatch : CustomPatch
    {
        public GS_StopElevatorRidePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_StopElevatorRide), nameof(GS_StopElevatorRide.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.StopElevatorRide;
        }
    }
}
