namespace CustomLevelProgression.Parsers
{
    public static class BooleanParser
    {
        public static bool Parse(string input)
        {
            return input != null && input.ToLower() == "true";
        }
    }
}