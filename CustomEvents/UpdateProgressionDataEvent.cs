using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;

namespace CustomLevelProgression.CustomEvents
{
    public class UpdateProgressionDataEvent : CustomEvent
    {
        public UpdateProgressionDataEvent() : base(10U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate UpdateProgressionDataEvent");

            string tier;
            string expeditionIndex;
            string value;

            info.Parameters.TryGetValue("Tier", out tier);
            info.Parameters.TryGetValue("ExpeditionIndex", out expeditionIndex);
            info.Parameters.TryGetValue("Value", out value);

            Activate(tier, expeditionIndex, value);
        }

        public void Activate(string tier = null, string expeditionIndex = null, string value = null)
        {
            Activate(eRundownTierParser.Parse(tier), Int32Parser.Parse(expeditionIndex), Int32Parser.Parse(value));
        }

        public void Activate(eRundownTier tier, int expeditionIndex, int value)
        {
            GameInfo.SetProgressionValue(tier, expeditionIndex, value);
        }
    }
}
