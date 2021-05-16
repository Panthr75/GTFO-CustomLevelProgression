using HarmonyLib;
using CellMenu;
using LevelGeneration;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_ExpeditionWindow_Patch : CustomPatch
    {
        public CM_ExpeditionWindow_Patch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_ExpeditionWindow), nameof(CM_ExpeditionWindow.SetVisible), new System.Type[] { typeof(bool), typeof(bool) })
        { }

        public static bool Invoke(CM_ExpeditionWindow __instance, bool visible, bool inMenuBar)
        {
            __instance.gameObject.SetActive(visible);
            __instance.m_joinWindow.SetVisible(!inMenuBar && !SNetwork.SNet.IsInLobby);
            __instance.m_hostButton.SetVisible(!inMenuBar && !SNetwork.SNet.IsInLobby && (__instance.m_status != eExpeditionIconStatus.TierLocked && __instance.m_status != eExpeditionIconStatus.LockedAndScrambled) && __instance.m_status != eExpeditionIconStatus.TierLockedFinishedAnyway);
            __instance.m_changeExpeditionButton.SetVisible(SNetwork.SNet.IsMaster && !__instance.m_hostButton.IsVisible && (!inMenuBar && SNetwork.SNet.IsInLobby) && (RundownManager.ActiveExpedition != null && __instance.m_status != eExpeditionIconStatus.TierLocked && __instance.m_status != eExpeditionIconStatus.LockedAndScrambled) && __instance.m_status != eExpeditionIconStatus.TierLockedFinishedAnyway);
            __instance.m_matchButton.SetVisible(!inMenuBar && !SNetwork.SNet.IsInLobby && (__instance.m_status != eExpeditionIconStatus.TierLocked && __instance.m_status != eExpeditionIconStatus.LockedAndScrambled) && __instance.m_status != eExpeditionIconStatus.TierLockedFinishedAnyway);
            __instance.m_bottomStripes.SetActive(!__instance.m_hostButton.IsVisible && !__instance.m_changeExpeditionButton.IsVisible);
            if (visible)
            {
                var allCompletionData = CompletionData.LoadFromCache();
                var completionData = allCompletionData.GetData(__instance.m_tier, __instance.m_expIndex);

                __instance.m_title.gameObject.SetActive(false);
                __instance.m_wardenObjective.gameObject.SetActive(false);
                __instance.m_wardenIntel.gameObject.SetActive(false);
                __instance.m_depth.gameObject.SetActive(false);
                CoroutineManager.BlinkIn(__instance.m_title.gameObject, 0.3f);
                CoroutineManager.BlinkIn(__instance.m_wardenObjective.gameObject, 1.1f);
                CoroutineManager.BlinkIn(__instance.m_wardenIntel.gameObject, 2.5f);
                CoroutineManager.BlinkIn(__instance.m_depth.gameObject, 3f);
                float delay1 = 1.8f;
                float num1 = 0.4f;
                __instance.UpdateProgression();
                __instance.m_sectorIconMain.m_isCleared = completionData.highCompletes > 0;
                __instance.m_sectorIconMain.SetVisible(false);
                __instance.m_sectorIconSecond.SetVisible(false);
                __instance.m_sectorIconThird.SetVisible(false);
                __instance.m_sectorIconAllCompleted.SetVisible(false);
                __instance.m_sectorIconMain.StopBlink();
                __instance.m_sectorIconSecond.StopBlink();
                __instance.m_sectorIconThird.StopBlink();
                __instance.m_sectorIconAllCompleted.StopBlink();
                __instance.m_sectorIconMain.BlinkIn(delay1);
                float delay2 = delay1 + num1;
                if (RundownManager.HasSecondaryLayer(__instance.m_data))
                {
                    __instance.m_sectorIconSecond.m_isCleared = completionData.highCompletes > 0;
                    __instance.m_sectorIconSecond.BlinkIn(delay2);
                    delay2 += num1;
                }
                if (RundownManager.HasThirdLayer(__instance.m_data))
                {
                    __instance.m_sectorIconThird.m_isCleared = completionData.overloadCompletes > 0;
                    __instance.m_sectorIconThird.BlinkIn(delay2);
                    delay2 += num1;
                }
                if (completionData.highCompletes > 0 && RundownManager.HasSecondaryLayer(__instance.m_data) && completionData.extremeCompletes > 0 && RundownManager.HasThirdLayer(__instance.m_data) && completionData.overloadCompletes > 0 && completionData.peCompletes > 0)
                {
                    __instance.m_sectorIconAllCompleted.BlinkIn(delay2);
                    float num2 = delay2 + num1;
                }
            }
            __instance.IsVisible = visible;

            return false;
        }
    }
}
