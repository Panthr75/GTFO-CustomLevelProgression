using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using CustomLevelProgression.IL2CPP;
using CustomLevelProgression.Lights;
using CustomLevelProgression.Patches;
using HarmonyLib;
using System;
using System.Reflection;

namespace CustomLevelProgression
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BasePlugin
    {
        public const string GUID = "dev.flaff.gtfo.CustomLevelProgression";
        public const string NAME = "CustomLevelProgression";
        public const string VERSION = "1.0.0";
        public const string AUTHOR = "Flaff";

        internal static ManualLogSource log;
        internal static Harmony harmony;

        public override void Load()
        {
            Il2CppTypeRegistry.Load();
            Il2CppTypeRegistry.Register<CoroutineHandler>();
            Il2CppTypeRegistry.Register<LightController>();

            log = Log;
            harmony = new Harmony(GUID);

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();
                var patchType = typeof(PatchBase);

                foreach (Type type in types)
                {
                    if (patchType.IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        var constructor = type.GetConstructor(new Type[] { typeof(Harmony) });
                        if (constructor != null)
                        {
                            try
                            {
                                constructor.Invoke(new object[] { harmony });
                                Log.LogInfo($"Created instance of patch '{type.FullName}'");
                            }
                            catch (Exception e)
                            {
                                Log.LogError($"Failed to create instance of Patch type '{type.FullName}'\nException: {e}");
                            }
                        }
                        else
                        {
                            Log.LogWarning($"Skipping '{type.FullName}' as it doesn't have a proper harmony constructor");
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log.LogError("Failed getting patches: " + e.StackTrace);
            }

            PatchBase.PatchAll();
        }

        public override bool Unload()
        {
            Il2CppTypeRegistry.Unload();
            PatchBase.UnpatchAll();

            return base.Unload();
        }
    }
}
