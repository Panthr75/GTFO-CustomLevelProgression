namespace CustomLevelProgression.DataBlocks
{
    public class ProgressionRequirement
    {
        public eRundownTier Tier { get; set; }
        public int ExpeditionIndex { get; set; }
        public int Value { get; set; }
        public ProgressionCheckType CheckType { get; set; }
    }
}