namespace CustomLevelProgression.Parsers
{
    public static class FloatParser
    {
        public static float Parse(string input)
        {
            float result;
            if (input == null || !float.TryParse(input, out result))
            {
                return 0f;
            }

            return result;
        }
    }
}