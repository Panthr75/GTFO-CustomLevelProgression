using CustomLevelProgression.Lights;
using CustomLevelProgression.Utilities;
using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class LayerLightSequenceOverrides : LightSequenceOverrides
    {
        public LG_LayerType Layer { get; set; }
        public List<ZoneLightSequenceOverrides> ZoneOverrides { get; set; }

        public override LightSequenceOverrides[] Children
        {
            get
            {
                var children = new LightSequenceOverrides[ZoneOverrides.Count];
                for (int index = 0; index < ZoneOverrides.Count; index++)
                {
                    children[index] = ZoneOverrides[index];
                }

                return children;
            }
        }

        public override bool CanChangeStateOf(LightController controller)
        {
            return controller != null && controller.HasArea && controller.LayerType == Layer;
        }
    }
}