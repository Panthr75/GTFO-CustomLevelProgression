using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using HarmonyLib;
using LevelGeneration;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_PowerGeneratorCorePatch : CustomPatch
    {
        public LG_PowerGeneratorCorePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(LG_PowerGenerator_Core), nameof(LG_PowerGenerator_Core.OnStateChange))
        { }

        public static void Invoke(LG_PowerGenerator_Core __instance, pPowerGeneratorState oldState, pPowerGeneratorState newState)
        {
            if (oldState.status != newState.status && newState.status == ePowerGeneratorStatus.Powered && __instance.ObjectiveItemSolved)
            {
                var blocks = EventListenerDataBlock.GetAllBlocks();
                var exp = RundownManager.GetActiveExpeditionData();

                foreach (var block in blocks)
                {
                    if (block.Type == EventListenerType.CellInsert)
                    {
                        foreach (var expedition in block.ForExpeditions)
                        {
                            if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                            {
                                if (__instance.SpawnNode.LayerType == block.InsertCell_Layer)
                                {
                                    var items = WardenObjectiveManager.Current.m_objectiveItemCollection[__instance.SpawnNode.LayerType];
                                    int count = 0;

                                    foreach (var item in items)
                                    {
                                        var powerGen = item?.TryCast<LG_PowerGenerator_Core>();
                                        if (powerGen != null && powerGen.ObjectiveItemSolved)
                                            count++;
                                    }

                                    if (block.InsertCell_RequiredCount == count)
                                    {
                                        EventSequenceManager.StartSequence(block.EventSequenceOnActivate);
                                    }
                                }
                                break;
                            }
                        }

                    }
                }
            }
        }
    }
}
