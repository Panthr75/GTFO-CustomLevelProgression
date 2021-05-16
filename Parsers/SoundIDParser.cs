using AK;

namespace CustomLevelProgression.Parsers
{
    public static class SoundIDParser
    {
        public static uint Parse(string input)
        {
            if (input == null)
            {
                return 0U;
            }

            uint soundID;
            if (!uint.TryParse(input, out soundID))
            {
                var eventsType = typeof(EVENTS);
                var field = eventsType.GetField(input.ToUpper());
                if (field != null)
                {
                    soundID = (uint)field.GetValue(null);
                }
                else
                {
                    var property = eventsType.GetField(input.ToUpper());
                    if (property != null)
                    {
                        soundID = (uint)property.GetValue(null);
                    }
                }
            }

            return soundID;
        }
    }
}