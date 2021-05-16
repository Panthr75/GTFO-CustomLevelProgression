using GameData;

namespace CustomLevelProgression.Parsers
{
    public static class eLocalZoneIndexParser
    {
        public static eLocalZoneIndex Parse(string input)
        {
            if (input == null)
            {
                return eLocalZoneIndex.Zone_0;
            }

            int zoneIndex;
            if (int.TryParse(input, out zoneIndex))
            {
                return (eLocalZoneIndex)zoneIndex;
            }

            input = input.ToLower();
            if (input.StartsWith("zone_"))
                input = input.Substring(5);

            if (int.TryParse(input, out zoneIndex))
            {
                return (eLocalZoneIndex)zoneIndex;
            }

            return eLocalZoneIndex.Zone_0;
        }
    }
}