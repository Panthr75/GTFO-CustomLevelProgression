namespace CustomLevelProgression.Parsers
{
    public static class Int32Parser
    {
        public static int Parse(string input)
        {
            int result;
            if (input == null || !int.TryParse(input, out result))
            {
                return 0;
            }

            return result;
        }
    }
}