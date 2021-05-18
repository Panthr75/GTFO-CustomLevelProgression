using AIGraph;
using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using Player;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_PickupItem_Sync_StateChangePatch : CustomPatch
    {
        public LG_PickupItem_Sync_StateChangePatch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(LG_PickupItem_Sync), nameof(LG_PickupItem_Sync.OnStateChange))
        { }

        public static void Invoke(LG_PickupItem_Sync __instance, pPickupItemState oldState, pPickupItemState newState)
        {
            if (oldState.status != newState.status && newState.status == ePickupItemStatus.PickedUp)
            {
                AIG_CourseNode spawnNode = __instance.item.TryCast<CarryItemPickup_Core>()?.SpawnNode ?? __instance.item.TryCast<GenericSmallPickupItem_Core>()?.SpawnNode;
                if (spawnNode == null)
                    return;

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
                                if (spawnNode.LayerType == block.ObjectiveItemSolved_Layer)
                                {
                                    var items = WardenObjectiveManager.Current.m_objectiveItemCollection[spawnNode.LayerType];
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
