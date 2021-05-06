using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EventSequenceDataBlock : CustomDataBlock<EventSequenceDataBlock>
    {
        public List<EventInfo> Events { get; set; }

    }
}