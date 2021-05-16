using CellMenu;
using CustomLevelProgression.Utilities;
using HarmonyLib;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_ExpeditionIconNew_Patch : CustomPatch
    {
        public CM_ExpeditionIconNew_Patch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_ExpeditionIcon_New), nameof(CM_ExpeditionIcon_New.SetStatus))
        { }

        public static void Invoke(CM_ExpeditionIcon_New __instance, ref eExpeditionIconStatus status, ref string mainFinishCount, ref string secondFinishCount, ref string thirdFinishCount, ref string allFinishedCount)
        {
            if (status == eExpeditionIconStatus.NotPlayed)
            {
                var allCompletionData = CompletionData.LoadFromCache();
                var completionData = allCompletionData.GetData(__instance.Tier, __instance.ExpIndex);

                var expDataBlock = __instance.DataBlock;

                if (completionData.highCompletes > 0 || completionData.extremeCompletes > 0 || completionData.overloadCompletes > 0 || completionData.peCompletes > 0)
                {
                    status = eExpeditionIconStatus.PlayedAndFinished;
                }

                mainFinishCount = completionData.highCompletes.ToString();
                secondFinishCount = RundownManager.HasSecondaryLayer(expDataBlock) ? completionData.extremeCompletes.ToString() : "-";
                thirdFinishCount = RundownManager.HasThirdLayer(expDataBlock) ? completionData.overloadCompletes.ToString() : "-";
                allFinishedCount = RundownManager.HasAllCompletetionPossibility(expDataBlock) ? completionData.peCompletes.ToString() : "-";


            }
        }
    }
}