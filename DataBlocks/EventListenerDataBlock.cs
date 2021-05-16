using GameData;
using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EventListenerDataBlock : CustomDataBlock<EventListenerDataBlock>
    {
        public EventListenerType Type { get; set; }
        public string Header { get; set; }
        public List<ListenerExpedition> ForExpeditions { get; set; }
        public uint EventSequenceOnActivate { get; set; }

        public bool LevelLoad_IncludeJoins { get; set; }
        public bool LevelLoad_IncludeInitialDrop { get; set; }

        public int ObjectiveItemSolved_RequiredCount { get; set; }
        public LG_LayerType ObjectiveItemSolved_Layer { get; set; }

        public List<LG_LayerType> CompleteObjective_Layers { get; set; }

        public LG_LayerType OpenSecurityDoor_Layer { get; set; }
        public eLocalZoneIndex OpenSecurityDoor_ZoneIndex { get; set; }
        public eLocalZoneIndex OpenSecurityDoor_BuildFromIndex { get; set; }
    }
}