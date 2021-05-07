using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using LevelGeneration;

namespace CustomLevelProgression
{
    public static class LightSequenceManager
    {
        public static void ActivateSequence(uint sequenceID)
        {
            Log.Message("Attempting to activate light sequence with id: " + sequenceID);
            var sequence = LightSequenceDataBlock.GetBlock(sequenceID);
            if (sequence != null)
            {
                Log.Message("Successfully got sequence block, activating...");
                sequence.Apply(/*Builder.CurrentFloor*/);
            }
        }
    }
}