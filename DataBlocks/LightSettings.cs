using UnityEngine;

namespace CustomLevelProgression.DataBlocks
{
    public class LightSettings
    {
        public bool OverrideBase { get; set; }

        public bool UseBaseIntensity { get; set; }
        public bool ModifyIntensity { get; set; }
        public float ModifiedIntensity { get; set; }
        public LightIntensityOperation IntensityOperation { get; set; }

        public bool UseBaseColor { get; set; }
        public bool ModifyColor { get; set; }
        public Color ModifiedColor { get; set; }
        public LightColorOperation ColorOperation { get; set; }
    }
}