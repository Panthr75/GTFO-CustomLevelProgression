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
    }
}