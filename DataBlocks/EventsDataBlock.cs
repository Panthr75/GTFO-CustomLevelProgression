using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EventsDataBlock : CustomDataBlock<EventsDataBlock>
    {
        public Dictionary<string, string> Parameters { get; set; }
    }
}