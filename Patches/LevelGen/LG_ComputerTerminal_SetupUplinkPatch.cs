using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using System;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_ComputerTerminal_SetupUplinkPatch : CustomPatch
    {
        public LG_ComputerTerminal_SetupUplinkPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(LG_ComputerTerminal), nameof(LG_ComputerTerminal.SetupAsWardenObjectiveTerminalUplink))
        { }

        public static void OnUplinkSolved(LG_ComputerTerminal __instance)
        {
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

        public static void Invoke(LG_ComputerTerminal __instance)
        {
            var listener = (Il2CppSystem.Action)new Action(() =>
            {
                OnUplinkSolved(__instance);
            });

            __instance.UplinkPuzzle.OnPuzzleSolved = __instance.UplinkPuzzle.OnPuzzleSolved == null ? listener : (Il2CppSystem.Action)__instance.UplinkPuzzle.OnPuzzleSolved.CombineImpl(listener);
        }
    }
}