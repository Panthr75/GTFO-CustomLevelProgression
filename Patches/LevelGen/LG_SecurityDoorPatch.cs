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
            if (state.status == eDoorStatus.Open)
            {
                var blocks = EventListenerDataBlock.GetAllBlocks();
                var exp = RundownManager.GetActiveExpeditionData();
                foreach (var block in blocks)
                {
                    if (block.Type == EventListenerType.SecurityDoorOpen)
                    {
                        foreach (var expedition in block.ForExpeditions)
                        {
                            if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                            {
                                var data = __instance.LinkedToZoneData;
                                if (__instance.LinksToLayerType == block.OpenSecurityDoor_Layer && data.LocalIndex == block.OpenSecurityDoor_ZoneIndex && data.BuildFromLocalIndex == block.OpenSecurityDoor_BuildFromIndex)
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
