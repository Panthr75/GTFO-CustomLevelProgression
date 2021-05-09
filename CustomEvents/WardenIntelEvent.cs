using CustomLevelProgression.DataBlocks;
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
            
            var ev = Event;
            string typeName;
            object text = null;
            object displayDuration = null;
            object isObjectiveText = null;

            if (ev.Parameters.TryGetValue("Text", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("Text", out text);

                if (text != null)
                    text = Convert.ChangeType(text, type);
            }

            if (ev.Parameters.TryGetValue("DisplayDuration", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("DisplayDuration", out displayDuration);

                if (displayDuration != null)
                    displayDuration = Convert.ChangeType(displayDuration, type);
            }

            if (ev.Parameters.TryGetValue("IsObjectiveText", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("IsObjectiveText", out isObjectiveText);

                if (isObjectiveText != null)
                    isObjectiveText = Convert.ChangeType(isObjectiveText, type);
            }

            Activate(text, displayDuration, isObjectiveText);
        }

        public void Activate(object text = null, object displayDuration = null, object isObjectiveText = null)
        {
            Activate(text == null ? "" : (string)text, displayDuration == null ? 0.0f : (float)displayDuration, isObjectiveText != null && (bool)isObjectiveText);
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