namespace CustomLevelProgression.DataBlocks
{
    public class LightStateInfo
    {
        public uint StateID { get; set; }
        public uint StateDelay { get; set; }
        public uint TransitionTime { get; set; }
        public uint NextStateDelay { get; set; }
        public SoundEventInfo SoundEvent { get; set; }
        public string name { get; set; }
    }
}