using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;
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
            Log.Message("Activate EventSequenceEvent");

            string sequenceID;

            info.Parameters.TryGetValue("SequenceID", out sequenceID);

            Activate(sequenceID);
        }

        public void Activate(string sequenceID = null)
        {
            Activate(UInt32Parser.Parse(sequenceID));
        }

        public void Activate(uint sequenceID)
        {
            EventSequenceManager.StartSequence(sequenceID);
        }
    }
}
