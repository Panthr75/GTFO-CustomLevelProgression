using CustomLevelProgression.Lights;
using CustomLevelProgression.Utilities;
using LevelGeneration;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class LightSequenceDataBlock : CustomDataBlock<LightSequenceDataBlock>
    {
        public uint LightState { get; set; }
        public bool UpdateState { get; set; }

        public List<LayerLightSequenceOverrides> LayerOverrides { get; set; }

        public static bool GetStateForController(LightController controller, LightSequenceOverrides overrides, ref uint state)
        {
            if (!overrides.CanChangeStateOf(controller))
                return false;

            if (overrides.UpdateState)
            {
                state = overrides.LightState;
            }

            var children = overrides.Children;
            if (children != null)
            {
                foreach (var child in children)
                {
                    if (GetStateForController(controller, child, ref state))
                    {
                        break;
                    }
                }
            }

            return true;
        }

        public void Apply()
        {
            uint state = UpdateState ? LightState : 0U;

            foreach (var controller in LightController.controllers)
            {
                uint controllerState = state;
                foreach (var layerOverride in LayerOverrides)
                {
                    if (GetStateForController(controller, layerOverride, ref controllerState))
                    {
                        break;
                    }
                }

                controller.ChangeState(LightStateDataBlock.GetBlock(controllerState));
            }
        }
    }
}