using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_InLevelPatch : CustomPatch
    {
        public GS_InLevelPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_InLevel), nameof(GS_InLevel.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.InLevel;

            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();
            foreach (var block in blocks)
            {
                Log.Message("Found block!");
                if (block.Type == EventListenerType.LevelLoad)
                {
                    Log.Message("Is Level Load!");
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            Log.Message("Found level!");
                            if (block.LevelLoad_IncludeInitialDrop)
                            {
                                Log.Message("Peforming event sequence...");
                                EventSequenceManager.StartSequence(block.EventSequenceOnActivate);
                            }
                            break;
                        }
                    }
                    
                }
            }
        }
    }
}
