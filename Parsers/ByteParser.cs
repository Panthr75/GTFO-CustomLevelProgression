namespace CustomLevelProgression.Parsers
{
    public static class ByteParser
    {
        public static byte Parse(string input)
        {
            byte result;
            if (input == null || !byte.TryParse(input, out result))
            {
                return 0;
            }

            return result;
        }
    }
}