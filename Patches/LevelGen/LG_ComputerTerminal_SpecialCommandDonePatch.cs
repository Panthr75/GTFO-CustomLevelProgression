
using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using System;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_ComputerTerminal_SpecialCommandDonePatch : CustomPatch
    {
        public LG_ComputerTerminal_SpecialCommandDonePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(LG_ComputerTerminal), nameof(LG_ComputerTerminal.OnWardenObjectiveSpecialCommandDone))
        { }

        public static void Invoke(LG_ComputerTerminal __instance)
        {
            if (__instance.m_chainPuzzleForWardenObjective != null)
            {
                __instance.m_chainPuzzleForWardenObjective.add_OnPuzzleSolved(new Action(() =>
                {
                    __instance.ObjectiveItemSolved = true;
                    Invoke(__instance);
                }));
                return;
            }

            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();

            foreach (var block in blocks)
            {
                if (block.Type == EventListenerType.ObjectiveItemSolved)
                {
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            if (__instance.SpawnNode.LayerType == block.ObjectiveItemSolved_Layer)
                            {
                                var items = WardenObjectiveManager.Current.m_objectiveItemCollection[__instance.SpawnNode.LayerType];
                                int count = 0;

                                foreach (var item in items)
                                {
                                    if (item != null && item.ObjectiveItemSolved)
                                        count++;
                                }

                                if (block.ObjectiveItemSolved_RequiredCount == count)
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
