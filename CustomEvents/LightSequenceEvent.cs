using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
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
            LightSequenceManager.ActivateSequence(sequenceID);
        }
    }
}