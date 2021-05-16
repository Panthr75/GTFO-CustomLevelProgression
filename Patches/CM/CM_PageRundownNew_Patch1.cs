using CellMenu;
using GameData;
using HarmonyLib;
using UnityEngine;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_PageRundownNew_Patch1 : CustomPatch
    {
        public CM_PageRundownNew_Patch1(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_PageRundown_New), nameof(CM_PageRundown_New.UpdateExpeditionIconProgression))
        { }

        public static bool Invoke(CM_PageRundown_New __instance)
        {
            var completionData = CompletionData.LoadFromCache();
            Utilities.Log.Message("Load completion data");

            Debug.Log("CM_PageRundown_New.UpdateRundownExpeditionProgression, RundownManager.RundownProgressionReady: " + RundownManager.RundownProgressionReady.ToString());

            int totalMain = 0;
            int totalSecond = 0;
            int totalThird = 0;
            int totalPE = 0;

            int totalCompletesMain = 0;
            int totalCompletesSecond = 0;
            int totalCompletesThird = 0;
            int totalCompletesPE = 0;

            if (__instance.m_expIconsAll != null)
            {
                for (int index = 0; index < __instance.m_expIconsAll.Count; index++)
                {
                    var icon = __instance.m_expIconsAll[index];
                    string expeditionKey = RundownManager.GetRundownProgressionExpeditionKey(icon.Tier, icon.ExpIndex);

                    var completes = completionData.GetData(icon.Tier, icon.ExpIndex);

                    if (completes.highCompletes > 0)
                        totalCompletesMain++;
                    if (completes.extremeCompletes > 0)
                        totalCompletesSecond++;
                    if (completes.overloadCompletes > 0)
                        totalCompletesThird++;
                    if (completes.peCompletes > 0)
                        totalCompletesPE++;

                    totalMain++;
                    if (RundownManager.HasSecondaryLayer(icon.DataBlock))
                        totalSecond++;
                    if (RundownManager.HasThirdLayer(icon.DataBlock))
                        totalThird++;
                    if (RundownManager.HasAllCompletetionPossibility(icon.DataBlock))
                        totalPE++;

                }
            }

            if (__instance.m_tierMarkerSectorSummary != null)
            {
                __instance.m_tierMarkerSectorSummary.SetSectorIconTextForMain(totalCompletesMain.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + totalMain + "</color></size>");
                __instance.m_tierMarkerSectorSummary.SetSectorIconTextForSecondary(totalCompletesSecond.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + totalSecond + "</color></size>");
                __instance.m_tierMarkerSectorSummary.SetSectorIconTextForThird(totalCompletesThird.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + totalThird + "</color></size>");
                __instance.m_tierMarkerSectorSummary.SetSectorIconTextForAllCleared(totalCompletesPE.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + totalPE + "</color></size>");
            }
            if (__instance.m_tierMarker1 == null)
                return false;

            var tierBReq = __instance.m_currentRundownData.ReqToReachTierB;
            var tierCReq = __instance.m_currentRundownData.ReqToReachTierC;
            var tierDReq = __instance.m_currentRundownData.ReqToReachTierD;
            var tierEReq = __instance.m_currentRundownData.ReqToReachTierE;

            var progressionData = new RundownManager.RundownProgData()
            {
                totalMain = totalMain,
                totalSecondary = totalSecond,
                totalThird = totalThird,
                totalAllClear = totalPE,

                clearedMain = totalCompletesMain,
                clearedSecondary = totalCompletesSecond,
                clearedThird = totalCompletesThird,
                clearedAllClear = totalCompletesPE,

                tierBUnlocked = totalCompletesMain >= tierBReq.MainSectors && totalCompletesSecond >= tierBReq.SecondarySectors && totalCompletesThird >= tierBReq.ThirdSectors && totalCompletesPE >= tierBReq.AllClearedSectors,
                tierCUnlocked = totalCompletesMain >= tierCReq.MainSectors && totalCompletesSecond >= tierCReq.SecondarySectors && totalCompletesThird >= tierCReq.ThirdSectors && totalCompletesPE >= tierCReq.AllClearedSectors,
                tierDUnlocked = totalCompletesMain >= tierDReq.MainSectors && totalCompletesSecond >= tierDReq.SecondarySectors && totalCompletesThird >= tierDReq.ThirdSectors && totalCompletesPE >= tierDReq.AllClearedSectors,
                tierEUnlocked = totalCompletesMain >= tierEReq.MainSectors && totalCompletesSecond >= tierEReq.SecondarySectors && totalCompletesThird >= tierEReq.ThirdSectors && totalCompletesPE >= tierEReq.AllClearedSectors
            };

            __instance.m_tierMarker1.SetProgression(progressionData, new RundownTierProgressionData());
            __instance.UpdateTierIconsWithProgression(null, __instance.m_expIconsTier1, __instance.m_tierMarker1, true);

            __instance.m_tierMarker2.SetProgression(progressionData, tierBReq);
            __instance.UpdateTierIconsWithProgression(null, __instance.m_expIconsTier2, __instance.m_tierMarker2, progressionData.tierBUnlocked);

            __instance.m_tierMarker3.SetProgression(progressionData, tierCReq);
            __instance.UpdateTierIconsWithProgression(null, __instance.m_expIconsTier3, __instance.m_tierMarker3, progressionData.tierCUnlocked);

            __instance.m_tierMarker4.SetProgression(progressionData, tierDReq);
            __instance.UpdateTierIconsWithProgression(null, __instance.m_expIconsTier4, __instance.m_tierMarker4, progressionData.tierDUnlocked);

            __instance.m_tierMarker5.SetProgression(progressionData, tierEReq);
            __instance.UpdateTierIconsWithProgression(null, __instance.m_expIconsTier5, __instance.m_tierMarker5, progressionData.tierEUnlocked);

            return false;
        }
    }
}
