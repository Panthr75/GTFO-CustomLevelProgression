using System;
using System.Collections.Generic;
using System.IO;
using CustomLevelProgression.Utilities;
using MTFO.Managers;
using Newtonsoft.Json;

namespace CustomLevelProgression.DataBlocks
{
    public class CustomDataBlock<T> where T : CustomDataBlock<T>
    {
        private static string filePath;
        public static CustomDataBlockWrapper<T> Wrapper;
        private static Dictionary<string, uint> blockIDsByName;
        private static Dictionary<uint, T> blocksByID;

        public string name { get; set; }
        public bool internalEnabled { get; set; }
        public uint persistentID { get; set; }

        public static string GetFilePath()
        {
            if (filePath == null)
            {
                if (ConfigManager.HasCustomContent)
                {
                    var progressionDataPath = Path.Combine(ConfigManager.CustomPath, "Progression_Data");
                    if (Directory.Exists(progressionDataPath))
                    {
                        var path = Path.Combine(progressionDataPath, "Progression_" + typeof(T).Name + "_bin.json");
                        if (File.Exists(path))
                        {
                            filePath = path;
                        }
                    }
                }
            }

            return filePath;
        }

        public static string GetFileContents()
        {
            var path = GetFilePath();
            if (path != null && File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        public static void Load()
        {
            string contents = GetFileContents();
            if (!string.IsNullOrEmpty(contents))
            {
                Wrapper = JsonConvert.DeserializeObject<CustomDataBlockWrapper<T>>(contents);
                if (blockIDsByName == null)
                    blockIDsByName = new Dictionary<string, uint>();
                else
                    blockIDsByName.Clear();

                if (blocksByID == null)
                    blocksByID = new Dictionary<uint, T>();
                else
                    blocksByID.Clear();

                if (Wrapper.Blocks != null)
                {
                    for (int index = 0; index < Wrapper.Blocks.Count; index++)
                    {
                        T block = Wrapper.Blocks[index];
                        if (blocksByID.ContainsKey(block.persistentID))
                        {
                            UnityEngine.Debug.Log($"Block {block.name} {block.persistentID} has a duplicate ID, reassigning. {typeof(T)}");
                            uint highestID = 0;
                            foreach (uint id in blocksByID.Keys)
                            {
                                if (id > highestID)
                                    highestID = id;
                            }
                            highestID++;

                            block.persistentID = highestID;
                            Wrapper.LastPersistentID = highestID;
                        }

                        if (blockIDsByName.ContainsKey(block.name))
                        {
                            UnityEngine.Debug.Log($"Block {block.name} {block.persistentID} has a duplicate NAME, renaming. {typeof(T)}");

                            int highestCopy = 1;
                            string newName = block.name + "_";
                            foreach (string name in blockIDsByName.Keys)
                            {
                                if (name.StartsWith(newName) && int.TryParse(name.Substring(newName.Length), out int copy))
                                {
                                    highestCopy = Math.Max(highestCopy, copy);
                                }
                            }

                            newName += highestCopy;
                            block.name = newName;
                        }

                        if (!blocksByID.ContainsKey(block.persistentID))
                        {
                            blocksByID.Add(block.persistentID, block);
                            Log.Message($"Added block to {typeof(T).Name}");
                        }
                        else
                        {
                            UnityEngine.Debug.Log($"ERROR: Trying to add block with duplicate id {block.persistentID} to blocksByID!");
                        }

                        if (!blockIDsByName.ContainsKey(block.name))
                        {
                            blockIDsByName.Add(block.name, block.persistentID);
                        }
                        else
                        {
                            UnityEngine.Debug.Log($"ERROR: Trying to add block with duplicate name {block.name} to blockIDsByName!");
                        }
                    }
                }
            }
        }

        public static bool IsLoaded() => Wrapper != null && Wrapper.Blocks != null;
        public static bool HasBlock(uint ID) => blocksByID != null && blocksByID.ContainsKey(ID) && blocksByID[ID].internalEnabled;
        public static bool HasBlock(string name) => blockIDsByName != null && blockIDsByName.ContainsKey(name) && blocksByID != null && blocksByID[blockIDsByName[name]].internalEnabled;

        public static T GetBlock(uint id)
        {
            if (id == 0U)
                return default;

            if (blocksByID.TryGetValue(id, out T block))
                return block;

            UnityEngine.Debug.Log($"Error: {typeof(T)} does not contain a block with ID: {id}");
            return default;
        }

        public static string GetBlockName(uint id)
        {
            if (blocksByID.TryGetValue(id, out T block))
                return block.name;

            UnityEngine.Debug.Log($"Error: {typeof(T)} does not contain a block with ID: {id}");
            return "";
        }

        public static T GetBlock(string name)
        {
            if (blockIDsByName.TryGetValue(name, out uint blockID))
                return GetBlock(blockID);

            UnityEngine.Debug.Log($"Error: {typeof(T)} does not contain a block with NAME: {name}");
            return default;
        }

        public static uint GetBlockID(string name) => blockIDsByName.ContainsKey(name) ? blockIDsByName[name] : 0U;

        public static T[] GetAllBlocks()
        {
            List<T> blocks = new List<T>();
            if (Wrapper.Blocks != null)
            {
                for (int index = 0; index < Wrapper.Blocks.Count; index++)
                {
                    if (Wrapper.Blocks[index].internalEnabled)
                        blocks.Add(Wrapper.Blocks[index]);
                }
            }

            return blocks.ToArray();
        }
    }
}