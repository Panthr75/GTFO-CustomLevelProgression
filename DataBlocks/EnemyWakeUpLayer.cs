using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class EnemyWakeUpLayer
    {
        public LG_LayerType Layer { get; set; }
        public bool Whitelist { get; set; }
        public List<EnemyWakeUpZone> Zones { get; set; }
    }
}