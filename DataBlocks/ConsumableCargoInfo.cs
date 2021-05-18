using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class ConsumableCargoInfo
    {
        public uint ItemID { get; set; }
        public float UsageRel { get; set; }
        public List<ProgressionRequirement> RequiredProgression { get; set; }
    }
}