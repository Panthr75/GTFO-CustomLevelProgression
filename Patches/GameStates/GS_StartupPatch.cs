using CustomLevelProgression.DataBlocks;
using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_StartupPatch : CustomPatch
    {
        public GS_StartupPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_Startup), nameof(GS_Startup.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.Startup;
            EventListenerDataBlock.Load();
            EventsDataBlock.Load();
            EventSequenceDataBlock.Load();
            LightSequenceDataBlock.Load();
            LightStatesDataBlock.Load();
        }
    }
}
