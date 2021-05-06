using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EventInfo
    {
        public uint EventID { get; set; }
        public bool RequiresProgression { get; set; }
        public List<ProgressionRequirement> RequiredProgression { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public bool SkipIfMaster { get; set; }
        public bool SkipIfClient { get; set; }
        public float ActivationDelay { get; set; }
        public float NextEventDelay { get; set; }
        public string name { get; set; }
    }
}