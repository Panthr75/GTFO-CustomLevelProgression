using CustomLevelProgression.DataBlocks;
using HarmonyLib;
using LevelGeneration;
using System.Collections.Generic;
using UnhollowerBaseLib;
using UnityEngine;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class ElevatorCargoCage_SpawnItemsPatch : CustomPatch
    {
        public ElevatorCargoCage_SpawnItemsPatch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(ElevatorCargoCage), nameof(ElevatorCargoCage.SpawnObjectiveItemsInLandingArea))
        { }

        public static bool Invoke(ElevatorCargoCage __instance)
        {
            var blocks = ExpeditionSettingsDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();

            foreach (var block in blocks)
            {
                if (block.Expedition.ExpeditionIndex == exp.expeditionIndex && block.Expedition.Tier == exp.tier)
                {
                    List<uint> bigPickupToSpawn = new List<uint>();
                    for (int index = 0; index < block.CargoCage.BigPickup.Count; index++)
                    {
                        var bigPickup = block.CargoCage.BigPickup[index];
                        if (GameInfo.MeetsProgressionRequirements(bigPickup.RequiredProgression))
                        {
                            bigPickupToSpawn.Add(bigPickup.ItemID);
                        }
                    }

                    List<ConsumableCargoInfo> consumablesToSpawn = new List<ConsumableCargoInfo>();
                    for (int index = 0; index < block.CargoCage.Consumables.Count; index++)
                    {
                        var consumable = block.CargoCage.Consumables[index];
                        if (GameInfo.MeetsProgressionRequirements(consumable.RequiredProgression))
                        {
                            consumablesToSpawn.Add(consumable);
                        }
                    }

                    int length = bigPickupToSpawn.Count + consumablesToSpawn.Count;

                    __instance.m_itemsToMoveToCargo = new Transform[length];

                    if (length < 1)
                        return false;

                    int itemIndex = 0;
                    foreach (var bigPickup in bigPickupToSpawn)
                    {
                        LG_PickupItem lgPickupItem = LG_PickupItem.SpawnGenericPickupItem(ElevatorShaftLanding.CargoAlign);
                        lgPickupItem.SpawnNode = Builder.GetElevatorArea().m_courseNode;
                        lgPickupItem.SetupAsBigPickupItem(Random.Range(0, int.MaxValue), bigPickup, false);
                        __instance.m_itemsToMoveToCargo[itemIndex] = lgPickupItem.transform;
                        itemIndex++;
                    }

                    foreach (var consumable in consumablesToSpawn)
                    {
                        LG_PickupItem lgPickupItem = LG_PickupItem.SpawnGenericPickupItem(ElevatorShaftLanding.CargoAlign);
                        lgPickupItem.SpawnNode = Builder.GetElevatorArea().m_courseNode;
                        lgPickupItem.SetupAsConsumable(Random.Range(0, int.MaxValue), consumable.ItemID);

                        __instance.m_itemsToMoveToCargo[itemIndex] = lgPickupItem.transform;
                        itemIndex++;

                        //Item item = ItemSpawnManager.SpawnItem(consumable.ItemID, ItemMode.Pickup, ElevatorShaftLanding.CargoAlign.position, ElevatorShaftLanding.CargoAlign.rotation, true, new Player.pItemData()
                        //{
                        //    custom = new Player.pItemData_Custom()
                        //    {
                        //        ammo = consumable.UsageRel
                        //    }
                        //}, ElevatorShaftLanding.CargoAlign);

                        //foreach (var comp in item.gameObject.GetComponentsInChildren<iLG_SpawnedInNodeHandler>(true))
                        //{
                        //    comp.SpawnNode = Builder.GetElevatorArea().m_courseNode;
                        //}

                        //__instance.m_itemsToMoveToCargo[itemIndex] = item.transform;
                    }

                    ElevatorRide.Current.m_cargoCageInUse = true;
                    return false;
                }
            }
            return true;
        }
    }
}
