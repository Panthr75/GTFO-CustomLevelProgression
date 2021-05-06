using GameData;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class ZoneLightOverride
    {
        public eLocalZoneIndex ZoneIndex { get; set; }
        public LightSettings Settings { get; set; }
        public List<AreaLightOverride> AreaOverrides { get; set; }
    }
}