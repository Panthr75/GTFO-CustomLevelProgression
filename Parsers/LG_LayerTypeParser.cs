using LevelGeneration;

namespace CustomLevelProgression.Parsers
{
    public static class LG_LayerTypeParser
    {
        public static LG_LayerType Parse(string input)
        {
            if (input == null)
            {
                return LG_LayerType.MainLayer;
            }

            if (byte.TryParse(input, out byte layerIndex))
            {
                return (LG_LayerType)layerIndex;
            }

            input = input.ToLower();
            if (input == "main" || input == "mainlayer" || input == "high")
                return LG_LayerType.MainLayer;
            else if (input == "second" || input == "secondlayer" || input == "extreme")
                return LG_LayerType.SecondaryLayer;
            else if (input == "third" || input == "thirdlayer" || input == "overload")
                return LG_LayerType.ThirdLayer;
            else
                return LG_LayerType.MainLayer;
        }
    }
}