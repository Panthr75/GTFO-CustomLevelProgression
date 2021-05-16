using GameData;

namespace CustomLevelProgression.Parsers
{
    public static class DataBlockIDParser<T> where T : GameDataBlockBase<T>
    {
        public static uint Parse(string input)
        {
            if (input == null)
            {
                return 0U;
            }
            
            if (uint.TryParse(input, out uint dbID))
            {
                if (GameDataBlockBase<T>.GetBlock(dbID) != null)
                {
                    return dbID;
                }
                else
                {
                    Utilities.Log.Warn("DataBlockIDParser: Failed to get enemy data block with id '" + dbID + "'");
                }
            }

            T namedBlock = GameDataBlockBase<T>.GetBlock(input);
            if (namedBlock != null)
            {
                return namedBlock.persistentID;
            }

            Utilities.Log.Warn("DataBlockIDParser: Failed to get enemy data block with name '" + input + "'");
            return 0U;
        }
    }
}