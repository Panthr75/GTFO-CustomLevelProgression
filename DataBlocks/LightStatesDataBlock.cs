using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class LightStatesDataBlock : CustomDataBlock<LightStatesDataBlock>
    {
        public float PercentOfLights { get; set; }
        
        public List<LayerLightOverride> LayerOverrides { get; set; }

        public LightSettings Settings { get; set; }
    }
}
