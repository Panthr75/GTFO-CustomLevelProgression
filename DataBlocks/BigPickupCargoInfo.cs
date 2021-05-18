using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class BigPickupCargoInfo
    {
        public uint ItemID { get; set; }
        public List<ProgressionRequirement> RequiredProgression { get; set; }
    }
}