using CustomLevelProgression.Lights;
using GameData;
using LevelGeneration;

namespace CustomLevelProgression.DataBlocks
{
    public abstract class LightSequenceOverrides
    {
        public bool UpdateState { get; set; }
        public uint LightState { get; set; }

        public abstract LightSequenceOverrides[] Children { get; }

        public abstract bool CanChangeStateOf(LightController controller);
    }
}