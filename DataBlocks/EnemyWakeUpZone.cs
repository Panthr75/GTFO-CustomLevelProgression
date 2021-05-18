using GameData;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EnemyWakeUpZone
    {
        public eLocalZoneIndex ZoneIndex { get; set; }
        public bool Whitelist { get; set; }
        public List<EnemyWakeUpArea> Areas { get; set; }
    }
}