using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;

namespace CustomLevelProgression.IL2CPP
{
    internal static class Il2CppTypeRegistry
    {
        private static List<Type> registeredTypes = new List<Type>();
        private static List<Il2CppBehaviour> behaviours = new List<Il2CppBehaviour>();

        internal static void Register<T>() where T : Il2CppSystem.Object
        {
            var type = typeof(T);
            if (!registeredTypes.Contains(type))
            {
                try
                {
                    ClassInjector.RegisterTypeInIl2Cpp<T>();
                    registeredTypes.Add(type);
                }
                catch(Exception e)
                {
                    Plugin.log.LogError($"Failed to register type '{type.FullName}' in il2cpp. THIS WILL BREAK STUFF\nException: {e}");
                }
            }
            else
            {
                Plugin.log.LogWarning($"Attempted to register {type.FullName} more than once.");
            }
        }

        internal static void AddBehaviour(Il2CppBehaviour behaviour)
        {
            if (!behaviours.Contains(behaviour))
            {
                behaviours.Add(behaviour);
            }
        }

        internal static void RemoveBehaviour(Il2CppBehaviour behaviour)
        {
            int index = behaviours.IndexOf(behaviour);
            if (index > -1)
            {
                behaviours.RemoveAt(index);
                behaviour.OnRemovedFromRegistry();
            }
        }

        internal static void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<Il2CppBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<Il2CppSingleton>();
        }

        internal static void Unload()
        {
            foreach (Il2CppBehaviour behaviour in behaviours)
            {
                behaviour.OnRemovedFromRegistry();
                if (behaviour is Il2CppSingleton singleton)
                {
                    singleton.DestroyInstance();
                }
            }

            behaviours.Clear();

            Plugin.log.LogWarning("Notice: Types can't be deregistered from Il2CPP. This may break stuff");
        }
    }
}
