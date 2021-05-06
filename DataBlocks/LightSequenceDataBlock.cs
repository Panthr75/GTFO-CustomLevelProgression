using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class LightSequenceDataBlock : CustomDataBlock<LightSequenceDataBlock>
    {
        public List<LightStateInfo> States { get; set; }
    }
}