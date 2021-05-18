using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class ProgressionUpdateInfo
    {
        public List<ProgressionRequirement> ProgressionRequirements { get; set; }
        public bool HighCompleted { get; set; }
        public bool ExtremeCompleted { get; set; }
        public bool OverloadCompleted { get; set; }
        public int Value { get; set; }
    }
}