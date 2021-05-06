using CellMenu;
using CustomLevelProgression.Utilities;
using GameData;
using HarmonyLib;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_PageRundownNew_Patch : CustomPatch
    {
        public CM_PageRundownNew_Patch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_PageRundown_New), nameof(CM_PageRundown_New.UpdateTierIconsWithProgression))
        { }

        public static void Invoke(CM_PageRundown_New __instance, Il2CppSystem.Collections.Generic.List<CM_ExpeditionIcon_New> tierIcons, CM_RundownTierMarker tierMarker, ref bool thisTierUnlocked)
        {
            if (tierIcons.Count > 0)
            {
                var tier = tierIcons[0].Tier;
                RundownTierProgressionData progressionReq = null;
                var completionData = CompletionData.LoadFromCache();
                var currentRundownData = __instance.m_currentRundownData;

                switch (tier)
                {
                    case eRundownTier.TierB:
                        progressionReq = currentRundownData?.ReqToReachTierB;
                        break;
                    case eRundownTier.TierC:
                        progressionReq = currentRundownData?.ReqToReachTierC;
                        break;
                    case eRundownTier.TierD:
                        progressionReq = currentRundownData?.ReqToReachTierD;
                        break;
                    case eRundownTier.TierE:
                        progressionReq = currentRundownData?.ReqToReachTierE;
                        break;
                }

                if (progressionReq != null)
                {
                    int high = completionData.TotalCompletes_High;
                    int extreme = completionData.TotalCompletes_Extreme;
                    int overload = completionData.TotalCompletes_Overload;
                    int pe = completionData.TotalCompletes_PE;

                    //__instance.m_tierMarkerSectorSummary.SetSectorIconTextForMain($"{high}<size=50%><color=#FFFFFF33><size=55%>/{progressionReq.MainSectors}</color></size>");
                    //__instance.m_tierMarkerSectorSummary.SetSectorIconTextForSecondary(num2.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + (object)num5 + "</color></size>");
                    //__instance.m_tierMarkerSectorSummary.SetSectorIconTextForThird(num3.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + (object)num6 + "</color></size>");
                    //__instance.m_tierMarkerSectorSummary.SetSectorIconTextForAllCleared(currAllClear.ToString() + "<size=50%><color=#FFFFFF33><size=55%>/" + (object)num7 + "</color></size>");

                    thisTierUnlocked = high >= progressionReq.MainSectors && extreme >= progressionReq.SecondarySectors && overload >= progressionReq.ThirdSectors && pe >= progressionReq.AllClearedSectors;
                    //tierMarker.SetStatus(thisTierUnlocked ? eRundownTierMarkerStatus.Unlocked : eRundownTierMarkerStatus.Locked);
                }
            }
        }
    }
}