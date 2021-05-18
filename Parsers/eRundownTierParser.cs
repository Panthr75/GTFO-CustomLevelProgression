

namespace CustomLevelProgression.Parsers
{
    public static class eRundownTierParser
    {
        public static eRundownTier Parse(string input)
        {
            if (input == null)
            {
                return eRundownTier.Surface;
            }

            if (int.TryParse(input, out int tierIndex))
            {
                return (eRundownTier)tierIndex;
            }

            input = input.ToLower();
            if (input == "tier_a" || input == "a" || input == "tiera")
                return eRundownTier.TierA;
            if (input == "tier_b" || input == "b" || input == "tierb")
                return eRundownTier.TierB;
            if (input == "tier_c" || input == "c" || input == "tierc")
                return eRundownTier.TierC;
            if (input == "tier_d" || input == "d" || input == "tierd")
                return eRundownTier.TierD;
            if (input == "tier_e" || input == "e" || input == "tiere")
                return eRundownTier.TierE;

            return eRundownTier.Surface;
        }
    }
}