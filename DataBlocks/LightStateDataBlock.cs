using UnityEngine;

namespace CustomLevelProgression.DataBlocks
{
    public class LightStateDataBlock : CustomDataBlock<LightStateDataBlock>
    {
        public uint NextStateID { get; set; }

        public float MinNextStateTime { get; set; }
        public float MaxNextStateTime { get; set; }

        public float MinActivationDelay { get; set; }
        public float MaxActivationDelay { get; set; }

        public float MinTransitionTime { get; set; }
        public float MaxTransitionTime { get; set; }

        public bool ChangeColor { get; set; }
        public bool ChangeIntensity { get; set; }

        public Color Color { get; set; }

        public bool RelativeIntensity { get; set; }
        public bool RelativeToStartIntensity { get; set; }

        public float MinIntensity { get; set; }
        public float MaxIntensity { get; set; }

        public bool LightAnimatorEnabled { get; set; }

    }
}