using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using System;
using System.Collections;

namespace CustomLevelProgression.CustomEvents
{
    public class LightSequenceEvent : CustomEvent
    {
        public LightSequenceEvent() : base(5U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate LightSequenceEvent");

            var ev = Event;
            string typeName;
            object sequenceID = null;

            if (ev.Parameters.TryGetValue("SequenceID", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("SequenceID", out sequenceID);

                if (sequenceID != null)
                    sequenceID = Convert.ChangeType(sequenceID, type);
            }

            Activate(sequenceID);
        }

        public void Activate(object sequenceID = null)
        {
            Activate(sequenceID == null ? 0U : (uint)sequenceID);
        }

        public void Activate(uint sequenceID)
        {
            LightSequenceManager.ActivateSequence(sequenceID);
        }
    }
}