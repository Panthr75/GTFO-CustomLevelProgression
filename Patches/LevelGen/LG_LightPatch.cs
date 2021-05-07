using CustomLevelProgression.Lights;
using HarmonyLib;
using LevelGeneration;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_LightPatch : CustomPatch
    {
        public LG_LightPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(LG_Light), nameof(LG_Light.SetEnabled), new System.Type[] { typeof(bool) })
        { }

        public static void Invoke(LG_Light __instance, bool enabled)
        {
            var gameObj = __instance.gameObject;
            var lightController = gameObj.GetComponent<LightController>();
            if (lightController == null)
                lightController = gameObj.AddComponent<LightController>();

            //lightController.enabled = __instance.AvailableInLevel;
        }
    }
}