namespace CustomLevelProgression.Parsers
{
    public static class UInt32Parser
    {
        public static uint Parse(string input)
        {
            uint result;
            if (input == null || !uint.TryParse(input, out result))
            {
                return 0U;
            }
            return result;
        }
    }
}