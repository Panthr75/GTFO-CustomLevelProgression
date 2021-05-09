using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using System;
using System.Collections;

namespace CustomLevelProgression.CustomEvents
{
    public class SoundEvent : CustomEvent
    {
        public SoundEvent() : base(7U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate SoundEvent");

            var ev = Event;
            string typeName;
            object sound = null;

            if (ev.Parameters.TryGetValue("Sound", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("Sound", out sound);

                if (sound != null)
                    sound = Convert.ChangeType(sound, type);
            }

            Activate(sound);
        }

        public void Activate(object sound = null)
        {
            Activate(sound == null ? 0U : (uint)sound);
        }

        public void Activate(uint sound)
        {
            CellSound.Post(sound, ExtendedPlayerAgent.LocalPlayer.Position);
        }
    }
}