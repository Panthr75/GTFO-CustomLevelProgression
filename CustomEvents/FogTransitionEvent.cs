using CustomLevelProgression.DataBlocks;
using System;
using GameData;
using System.Collections;
using UnityEngine;
using CustomLevelProgression.Utilities;
using CustomLevelProgression.Parsers;

namespace CustomLevelProgression.CustomEvents
{
    public class FogTransitionEvent : CustomEvent
    {
        public FogTransitionEvent() : base(3U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate FogTransitionEvent");

            string fogID;
            string transitionDelay;
            string transitionDuration;

            info.Parameters.TryGetValue("FogID", out fogID);
            info.Parameters.TryGetValue("TransitionDelay", out transitionDelay);
            info.Parameters.TryGetValue("TransitionDuration", out transitionDuration);

            Activate(fogID, transitionDelay, transitionDuration);
        }

        public void Activate(string fogID = null, string transitionDelay = null, string transitionDuration = null)
        {
            Activate(DataBlockIDParser<FogSettingsDataBlock>.Parse(fogID), FloatParser.Parse(transitionDelay), FloatParser.Parse(transitionDuration));
        }

        public void Activate(uint fogID, float transitionDelay, float transitionDuration)
        {
            GameInfo.StartCoroutine(ActivateSequence(fogID, transitionDelay, transitionDuration), true);
        }

        private IEnumerator ActivateSequence(uint fogID, float transitionDelay, float transitionDuration)
        {
            var fogDataBlock = FogSettingsDataBlock.GetBlock(fogID);
            if (fogDataBlock != null)
            {
                if (transitionDelay > 0.0f)
                    yield return new WaitForSeconds(transitionDelay);

                EnvironmentStateManager.AttemptStartFogTransition(fogID, transitionDuration);
            }
        }
    }
}
