using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using Player;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class CarryItemPickup_Core_StateChangePatch : CustomPatch
    {
        public CarryItemPickup_Core_StateChangePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(CarryItemPickup_Core), nameof(CarryItemPickup_Core.OnSyncStateChange))
        { }

        public static void Invoke(CarryItemPickup_Core __instance, ePickupItemStatus status)
        {
            if (status == ePickupItemStatus.PickedUp)
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
        }
    }
}
