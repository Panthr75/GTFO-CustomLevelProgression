using CustomLevelProgression.DataBlocks;
using System;

namespace CustomLevelProgression.CustomEvents
{
    public abstract class CustomEvent
    {
        private readonly uint id;
        public uint ID => id;
        public EventsDataBlock Event
        {
            get
            {
                var blocks = EventsDataBlock.GetAllBlocks();
                for (int index = 0; index < blocks.Length; index++)
                {
                    if (blocks[index].persistentID == id)
                        return blocks[index];
                }

                UnityEngine.Debug.Log($"ERROR: Failed to find EventsDataBlock for id {id}.");
                return null;
            }
        }

        public CustomEvent(uint id)
        {
            this.id = id;
        }

        public abstract void Activate(EventInfo info);

        public static Type SearchForType(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var tt = assembly.GetType(name);
                if (tt != null)
                {
                    return tt;
                }
            }

            return null;
        }
    }
}