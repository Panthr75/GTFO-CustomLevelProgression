using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_SecurityDoorPatch : CustomPatch
    {
        public LG_SecurityDoorPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(LG_SecurityDoor), nameof(LG_SecurityDoor.OnSyncDoorStatusChange))
        { }

        public static void Invoke(LG_SecurityDoor __instance, pDoorState state)
        {
            bool valid;
            EventListenerType validListenerType;

            switch (state.status)
            {
                case eDoorStatus.Open:
                    valid = true;
                    validListenerType = EventListenerType.SecurityDoorOpen;
                    break;
                case eDoorStatus.Unlocked:
                    valid = true;
                    validListenerType = EventListenerType.SecurityDoorUnlock;
                    break;
                case eDoorStatus.ChainedPuzzleActivated:
                    valid = true;
                    validListenerType = EventListenerType.SecurityDoorActivateChainedPuzzle;
                    break;
                default:
                    valid = false;
                    validListenerType = (EventListenerType)0;
                    break;
            }

            if (valid)
            {
                var blocks = EventListenerDataBlock.GetAllBlocks();
                var exp = RundownManager.GetActiveExpeditionData();
                foreach (var block in blocks)
                {
                    if (block.Type == validListenerType)
                    {
                        foreach (var expedition in block.ForExpeditions)
                        {
                            if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                            {
                                var data = __instance.LinkedToZoneData;
                                if (__instance.LinksToLayerType == block.SecurityDoor_Layer && data.LocalIndex == block.SecurityDoor_ZoneIndex && data.BuildFromLocalIndex == block.SecurityDoor_BuildFromIndex)
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
}
