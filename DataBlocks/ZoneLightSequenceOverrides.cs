using CustomLevelProgression.Lights;
using CustomLevelProgression.Utilities;
using GameData;
using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class ZoneLightSequenceOverrides : LightSequenceOverrides
    {
        public eLocalZoneIndex ZoneIndex { get; set; }
        public List<AreaLightSequenceOverrides> AreaOverrides { get; set; }

        public override LightSequenceOverrides[] Children
        {
            get
            {
                var children = new LightSequenceOverrides[AreaOverrides.Count];
                for (int index = 0; index < AreaOverrides.Count; index++)
                {
                    children[index] = AreaOverrides[index];
                }

                return children;
            }
        }

        public override bool CanChangeStateOf(LightController controller)
        {
            return controller != null && controller.HasArea && controller.ZoneIndex == ZoneIndex;
        }
    }
}