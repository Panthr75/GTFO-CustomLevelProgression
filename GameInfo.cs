using CustomLevelProgression.DataBlocks;
using GameData;
using LevelGeneration;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomLevelProgression
{
    public static class GameInfo
    {
        private static GameState state;
        public static GameState State
        {
            get => state;
            internal set
            {
                if (value != state)
                {
                    state = value;
                    Plugin.log.LogInfo($"Changed state to {value}");
                }
            }
        }
        public static bool InLevel => State == GameState.InLevel;

        public static int GetProgressionValue(eRundownTier tier, int expeditionIndex)
        {
            return ExtendedPlayerAgent.LocalPlayer.SessionProgressionData.GetProgressionValue(tier, expeditionIndex);
        }

        public static void SetProgressionValue(eRundownTier tier, int expeditionIndex, int value)
        {
            ExtendedPlayerAgent.LocalPlayer.ProgressionData.SetProgressionValue(tier, expeditionIndex, value);
        }

        public static void GetLocalProgressionValue(eRundownTier tier, int expeditionIndex, int value)
        {
            ExtendedPlayerAgent.LocalPlayer.ProgressionData.SetProgressionValue(tier, expeditionIndex, value);
        }

        public static bool MeetsProgressionRequirement(ProgressionRequirement requirement)
        {
            if (requirement == null)
                return true;

            int value = GetProgressionValue(requirement.Tier, requirement.ExpeditionIndex);
            
            switch (requirement.CheckType)
            {
                case ProgressionCheckType.Equals:
                    return value == requirement.Value;
                case ProgressionCheckType.NotEquals:
                    return value != requirement.Value;
                case ProgressionCheckType.GreaterThan:
                    return value > requirement.Value;
                case ProgressionCheckType.GreaterThanOrEqual:
                    return value >= requirement.Value;
                case ProgressionCheckType.LessThan:
                    return value < requirement.Value;
                case ProgressionCheckType.LessThanOrEqual:
                    return value <= requirement.Value;
                default:
                    return false;
            }
        }

        public static bool MeetsProgressionRequirements(IEnumerable<ProgressionRequirement> requirements)
        {
            foreach (ProgressionRequirement requirement in requirements)
            {
                if (!MeetsProgressionRequirement(requirement))
                {
                    return false;
                }
            }

            return true;
        }

        public static void StartCoroutine(IEnumerator routine, bool stopIfExitLevel = false)
        {
            CoroutineHandler.Create();
            CoroutineHandler.Add(routine, stopIfExitLevel);
        }

        public static LG_SecurityDoor GetSecurityDoor(LG_LayerType layerType, eLocalZoneIndex doorZoneIndex, eLocalZoneIndex buildFromZoneIndex)
            => GetSecurityDoorFromZone(GetZoneFromLayer(GetLayerFromFloor(Builder.CurrentFloor, layerType), buildFromZoneIndex), doorZoneIndex);

        public static LG_Layer GetLayerFromFloor(LG_Floor floor, LG_LayerType layerType)
        {
            if (floor == null)
                return null;

            for (int index = 0; index < floor.m_layers.Count; index++)
            {
                var layer = floor.m_layers[index];
                if (layer.m_type == layerType)
                    return layer;
            }

            return null;
        }

        public static LG_Zone GetZoneFromLayer(LG_Layer layer, eLocalZoneIndex index)
        {
            if (layer == null)
                return null;
            try
            {
                return layer.m_zonesByLocalIndex[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static LG_SecurityDoor GetSecurityDoorFromZone(LG_Zone zone, eLocalZoneIndex doorIndex)
        {
            if (zone == null)
                return null;

            for (int areaIndex = 0; areaIndex < zone.m_areas.Count; areaIndex++)
            {
                var area = zone.m_areas[areaIndex];
                for (int gateIndex = 0; gateIndex < area.m_gates.Count; gateIndex++)
                {
                    var gate = area.m_gates[gateIndex];
                    var door = gate?.SpawnedDoor;
                    if (door != null && (door.DoorType == eLG_DoorType.Security || door.DoorType == eLG_DoorType.Apex))
                    {
                        var securityDoor = door.TryCast<LG_SecurityDoor>();
                        if (securityDoor != null && securityDoor.LinkedToZoneData.LocalIndex == doorIndex)
                        {
                            return securityDoor;
                        }
                    }
                }
            }

            return null;
        }

        public static void UnlockSecurityDoor(LG_SecurityDoor door)
        {
            if (door != null)
            {
                if (door.m_locks != null)
                {
                    var locks = door.m_locks;
                    if (locks.ChainedPuzzleToSolve != null)
                    {
                        var puzzle = door.m_locks.ChainedPuzzleToSolve;
                        if (puzzle.Data.TriggerAlarmOnActivate)
                        {
                            door.m_sync.AttemptDoorInteraction(eDoorInteractionType.SetLockedWithChainedPuzzle_Alarm);
                            return;
                        }
                        else
                        {
                            door.m_sync.AttemptDoorInteraction(eDoorInteractionType.SetLockedWithChainedPuzzle);
                            return;
                        }
                    }
                }
                door.m_sync.AttemptDoorInteraction(eDoorInteractionType.Unlock);
            }
        }
    }
}