using GameData;
using System.Collections.Generic;

namespace CustomLevelProgression.DataBlocks
{
    public class CustomDataBlockWrapper<T> where T : CustomDataBlock<T>
    {
        public List<GameDataBlockListHeader> Headers { set; get; }
        public List<T> Blocks { set; get; }
        public uint LastPersistentID { set; get; }
    }
}