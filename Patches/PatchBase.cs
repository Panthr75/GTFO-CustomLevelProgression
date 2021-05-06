using System;
using System.Collections.Generic;

namespace CustomLevelProgression.Patches
{
    public abstract class PatchBase
    {
        private static List<PatchBase> patches = new List<PatchBase>();
        public abstract bool Patched { get; }

        public PatchBase()
        {
            patches.Add(this);
        }

        public abstract void Patch();
        public abstract void UnPatch();

        public static void PatchAll()
        {
            for (int index = 0; index < patches.Count; index++)
            {
                var patch = patches[index];
                if (!patch.Patched)
                {
                    try
                    {
                        patch.Patch();
                        Plugin.log.LogInfo($"Successfully created patch for patch type '{patch.GetType().FullName}'");
                    }
                    catch(Exception e)
                    {
                        Plugin.log.LogError($"Failed to create patch for patch type '{patch.GetType().FullName}'\nException: {e}");
                    }
                }
            }
        }

        public static void UnpatchAll()
        {
            for (int index = 0; index < patches.Count; index++)
            {
                var patch = patches[index];
                if (patch.Patched)
                    patch.UnPatch();
            }
        }
    }
}