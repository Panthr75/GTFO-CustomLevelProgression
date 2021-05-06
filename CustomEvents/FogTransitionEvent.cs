using CustomLevelProgression.DataBlocks;
using System;
using GameData;
using System.Collections;
using UnityEngine;

namespace CustomLevelProgression.CustomEvents
{
    public class FogTransitionEvent : CustomEvent
    {
        public FogTransitionEvent() : base(3U)
        { }

        public override void Activate(EventInfo info)
        {
            var ev = Event;
            string typeName;
            object fogID = null;
            object transitionDelay = null;
            object transitionDuration = null;

            if (ev.Parameters.TryGetValue("FogID", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("FogID", out fogID);

                if (fogID != null)
                    fogID = Convert.ChangeType(fogID, type);
            }

            if (ev.Parameters.TryGetValue("TransitionDelay", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("TransitionDelay", out transitionDelay);

                if (transitionDelay != null)
                    transitionDelay = Convert.ChangeType(transitionDelay, type);
            }

            if (ev.Parameters.TryGetValue("TransitionDuration", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("TransitionDuration", out transitionDuration);

                if (transitionDuration != null)
                    transitionDuration = Convert.ChangeType(transitionDuration, type);
            }

            Activate(fogID, transitionDelay, transitionDuration);
        }

        public void Activate(object fogID = null, object transitionDelay = null, object transitionDuration = null)
        {
            Activate(fogID == null ? 0U : (uint)fogID, transitionDelay == null ? 0.0f : (float)transitionDelay, transitionDuration == null ? 0.0f : (float)transitionDuration);
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

                if (transitionDuration <= 0.0f)
                {
                    LocalPlayerAgentSettings.Current.SetFogSettings(fogDataBlock);
                }
                else
                {
                    LocalPlayerAgentSettings.Current.SetTargetFogSettings(fogDataBlock);

                    float currentFogBlend = 0f;

                    while (currentFogBlend < 1.0f)
                    {
                        currentFogBlend += Clock.Delta * (1f / transitionDuration);
                        LocalPlayerAgentSettings.Current.UpdateBlendTowardsTargetFogSetting(currentFogBlend);
                        yield return null;
                    }
                }
            }
        }
    }
}
