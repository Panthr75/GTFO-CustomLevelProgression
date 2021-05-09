using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;

namespace CustomLevelProgression.Patches.Warden
{
    public class WardenObjectiveManager_Patch : CustomPatch
    {
        public WardenObjectiveManager_Patch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(WardenObjectiveManager), nameof(WardenObjectiveManager.OnStateChange), new System.Type[] { typeof(pWardenObjectiveState), typeof(pWardenObjectiveState), typeof(bool) })
        { }

        public static void Invoke(pWardenObjectiveState oldState, pWardenObjectiveState newState)
        {
            bool mainComplete = oldState.main_status != newState.main_status && newState.main_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;
            bool secondComplete = oldState.second_status != newState.second_status && newState.second_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;
            bool thirdComplete = oldState.third_status != newState.third_status && newState.third_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;

            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();
            foreach (var block in blocks)
            {
                if (block.Type == EventListenerType.ObjectiveComplete)
                {
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            if ((!block.CompleteObjective_Layers.Contains(LG_LayerType.MainLayer) || mainComplete) && (!block.CompleteObjective_Layers.Contains(LG_LayerType.SecondaryLayer) || secondComplete) && (!block.CompleteObjective_Layers.Contains(LG_LayerType.ThirdLayer) || thirdComplete))
                            {
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
