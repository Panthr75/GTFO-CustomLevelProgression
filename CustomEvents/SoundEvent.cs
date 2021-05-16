using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;

namespace CustomLevelProgression.CustomEvents
{
    public class SoundEvent : CustomEvent
    {
        public SoundEvent() : base(7U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate SoundEvent");

            string sound;

            info.Parameters.TryGetValue("Sound", out sound);

            Activate(sound);
        }

        public void Activate(string sound = null)
        {
            Activate(sound == null ? 0U : SoundIDParser.Parse(sound));
        }

        public void Activate(uint sound)
        {
            CellSound.Post(sound, ExtendedPlayerAgent.LocalPlayer.Position);
        }
    }
}