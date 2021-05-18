using GameData;
using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EventListenerDataBlock : CustomDataBlock<EventListenerDataBlock>
    {
        public EventListenerType Type { get; set; }
        public string Header { get; set; }
        public List<GeneralExpeditionInfo> ForExpeditions { get; set; }
        public uint EventSequenceOnActivate { get; set; }

        public bool LevelLoad_IncludeJoins { get; set; }
        public bool LevelLoad_IncludeInitialDrop { get; set; }

        public int ObjectiveItemSolved_RequiredCount { get; set; }
        public LG_LayerType ObjectiveItemSolved_Layer { get; set; }

        public List<LG_LayerType> CompleteObjective_Layers { get; set; }

        public LG_LayerType SecurityDoor_Layer { get; set; }
        public eLocalZoneIndex SecurityDoor_ZoneIndex { get; set; }
        public eLocalZoneIndex SecurityDoor_BuildFromIndex { get; set; }

        public List<EnemyWakeUpLayer> EnemyWakeUp_ForLayers { get; set; }
        public uint EnemyWakeup_EnemyID { get; set; }
    }
}