using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_ReadyToStopElevatorRidePatch : CustomPatch
    {
        public GS_ReadyToStopElevatorRidePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_ReadyToStopElevatorRide), nameof(GS_ReadyToStopElevatorRide.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.ReadyToStopElevatorRide;
        }
    }
}
