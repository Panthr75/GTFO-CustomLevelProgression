using CustomLevelProgression.Lights;
using CustomLevelProgression.Utilities;
using GameData;
using LevelGeneration;

namespace CustomLevelProgression.DataBlocks
{
    public class AreaLightSequenceOverrides : LightSequenceOverrides
    {
        public string AreaName { get; set; }

        public override LightSequenceOverrides[] Children => null;

        public override bool CanChangeStateOf(LightController controller)
        {
            return controller != null && controller.HasArea && controller.AreaName.ToLower() == AreaName?.ToLower();
        }
    }
}