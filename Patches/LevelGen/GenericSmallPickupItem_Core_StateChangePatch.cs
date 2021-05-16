using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using Player;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class GenericSmallPickupItem_Core_StateChangePatch : CustomPatch
    {
        public GenericSmallPickupItem_Core_StateChangePatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GenericSmallPickupItem_Core), nameof(GenericSmallPickupItem_Core.OnSyncStateChange))
        { }

        public static void Invoke(GenericSmallPickupItem_Core __instance, ePickupItemStatus status)
        {
            if (status == ePickupItemStatus.PickedUp)
            {
                var blocks = EventListenerDataBlock.GetAllBlocks();
                var exp = RundownManager.GetActiveExpeditionData();

                foreach (var block in blocks)
                {
                    if (block.Type == EventListenerType.ObjectiveItemSolved)
                    {
                        uint itemId = WardenObjectiveManager.ActiveWardenObjective(__instance.SpawnNode.LayerType).Gather_ItemId;

                        foreach (var expedition in block.ForExpeditions)
                        {
                            if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                            {
                                if (__instance.SpawnNode.LayerType == block.ObjectiveItemSolved_Layer)
                                {
                                    int count = 0;
                                    for (int index = 0; index < SNetwork.SNet.Slots.SlottedPlayers.Count; index++)
                                    {
                                        SNetwork.SNet_Player slottedPlayer = SNetwork.SNet.Slots.SlottedPlayers[index];
                                        if (slottedPlayer.IsInSlot)
                                        {
                                            PlayerBackpack backpack = PlayerBackpackManager.GetBackpack(slottedPlayer);
                                            count += backpack.CountPocketItem(itemId);
                                        }
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
