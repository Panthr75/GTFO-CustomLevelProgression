using CustomLevelProgression.DataBlocks;

namespace CustomLevelProgression
{
    public class CustomLightState
    {
        private readonly uint id;
        public uint ID => id;
        public LightStatesDataBlock DataBlock => LightStatesDataBlock.GetBlock(this.id);

        public CustomLightState(uint id)
        {
            this.id = id;
        }
    }
}