using System;
using System.Collections.Generic;

namespace CustomLevelProgression
{
    public static class LightSequenceManager
    {
        private static Dictionary<uint, CustomLightState> states;

        static LightSequenceManager()
        {
            states = new Dictionary<uint, CustomLightState>();
        }

        public static void StartSequence(uint sequenceID)
        {
            //
        }
    }
}