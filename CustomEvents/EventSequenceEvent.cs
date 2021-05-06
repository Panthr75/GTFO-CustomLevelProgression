using CustomLevelProgression.DataBlocks;
using System;
using System.Collections;

namespace CustomLevelProgression.CustomEvents
{
    public class EventSequenceEvent : CustomEvent
    {
        public EventSequenceEvent() : base(8U)
        { }

        public override void Activate(EventInfo info)
        {
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
            GameInfo.StartCoroutine(ActivateSequence(sequenceID), true);
        }

        private IEnumerator ActivateSequence(uint sequenceID)
        {
            EventSequenceManager.StartSequence(sequenceID);
            yield return null;
        }
    }
}
