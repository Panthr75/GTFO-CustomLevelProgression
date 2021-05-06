using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class LayerLightOverride
    {
        public LG_LayerType Layer { get; set; }
        public LightSettings Settings { get; set; }
        public List<ZoneLightOverride> ZoneOverrides { get; set; }
    }
}