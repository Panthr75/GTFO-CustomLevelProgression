using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;
using System;
using System.Collections;

namespace CustomLevelProgression.CustomEvents
{
    public class WardenIntelEvent : CustomEvent
    {
        public WardenIntelEvent() : base(4U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate WardenIntelEvent");
            
            string text;
            string displayDuration;
            string isObjectiveText;

            info.Parameters.TryGetValue("Text", out text);
            info.Parameters.TryGetValue("DisplayDuration", out displayDuration);
            info.Parameters.TryGetValue("IsObjectiveText", out isObjectiveText);

            Activate(text, displayDuration, isObjectiveText);
        }

        public void Activate(string text = null, string displayDuration = null, string isObjectiveText = null)
        {
            Activate(text == null ? "" : text, FloatParser.Parse(displayDuration), BooleanParser.Parse(isObjectiveText));
        }

        public void Activate(string text, float displayDuration, bool isObjectiveText)
        {
            Log.Message($"Activating WardenIntelEvent (text: {text}, displayDuration: {displayDuration}, isObjectiveText: {isObjectiveText})");

            var intel = GuiManager.PlayerLayer.m_wardenIntel;
            if (intel != null)
            {
                if (isObjectiveText)
                    intel.SetWardenObjectiveText(text);
                else
                    intel.SetIntelText(text);

                intel.SetVisible(true, displayDuration);
            }
        }
    }
}