using CellMenu;
using HarmonyLib;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_PageRundownNew_Patch1 : CustomPatch
    {
        public CM_PageRundownNew_Patch1(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_PageRundown_New), nameof(CM_PageRundown_New.UpdateExpeditionIconProgression))
        { }

        public static void Invoke(CM_PageRundown_New __instance)
        {
            var completionData = CompletionData.LoadFromCache();

            var progression = RundownManager.RundownProgression;
            if (progression != null && __instance.m_expIconsAll != null)
            {
                for (int index = 0; index < __instance.m_expIconsAll.Count; index++)
                {
                    var icon = __instance.m_expIconsAll[index];
                    string expeditionKey = RundownManager.GetRundownProgressionExpeditionKey(icon.Tier, icon.ExpIndex);

                    if (progression.Expeditions.ContainsKey(expeditionKey))
                    {
                        var expedition = progression.Expeditions[expeditionKey];
                        var completes = completionData.GetData(icon.Tier, icon.ExpIndex);

                        var main = expedition.Layers.Main;
                        var second = expedition.Layers.Secondary;
                        var third = expedition.Layers.Third;

                        main.CompletionCount = completes.highCompletes;
                        second.CompletionCount = completes.extremeCompletes;
                        third.CompletionCount = completes.overloadCompletes;

                        expedition.AllLayerCompletionCount = completes.peCompletes;
                    }
                }
            }
        }
    }
}
