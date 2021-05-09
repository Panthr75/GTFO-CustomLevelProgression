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

            foreach (var light in UnityEngine.Object.FindObjectsOfType<LevelGeneration.LG_Light>())
            {
                if (light.AvailableInLevel)
                {
                    if (light.gameObject.GetComponent<Lights.LightController>() == null)
                        light.gameObject.AddComponent<Lights.LightController>();
                }

            }

            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();
            foreach (var block in blocks)
            {
                if (block.Type == EventListenerType.LevelLoad)
                {
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            Log.Message($"Start Level Load Event Sequence for listener id {block.persistentID} ({block.name})!");
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
