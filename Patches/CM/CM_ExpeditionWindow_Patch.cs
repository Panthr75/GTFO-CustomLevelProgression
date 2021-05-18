using HarmonyLib;
using CellMenu;
using LevelGeneration;

namespace CustomLevelProgression.Patches.CM
{
    public class CM_ExpeditionWindow_Patch : CustomPatch
    {
        public CM_ExpeditionWindow_Patch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(CM_ExpeditionWindow), nameof(CM_ExpeditionWindow.SetVisible), new System.Type[] { typeof(bool), typeof(bool) })
        { }

        private static void SetupIcon(CM_ExpeditionSectorIcon icon, bool completed, LG_LayerType layer)
        {
            var bgIcon = icon.m_iconMainBG;
            var skullIcon = icon.m_iconMainSkull;

            switch (layer)
            {
                case LG_LayerType.SecondaryLayer:
                    bgIcon = icon.m_iconSecondaryBG;
                    skullIcon = icon.m_iconSecondarySkull;
                    break;
                case LG_LayerType.ThirdLayer:
                    bgIcon = icon.m_iconThirdBG;
                    skullIcon = icon.m_iconThirdSkull;
                    break;
            }

            var color = bgIcon.color;
            color.a = completed ? 0.5f : 0.3f;
            bgIcon.color = color;

            color = skullIcon.color;
            color.a = completed ? 0.8f : 0.4f;
            bgIcon.color = color;

            icon.m_isCleared = completed;
        }

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
                SetupIcon(__instance.m_sectorIconMain, completionData.highCompletes > 0, LG_LayerType.MainLayer);
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
                    SetupIcon(__instance.m_sectorIconSecond, completionData.extremeCompletes > 0, LG_LayerType.SecondaryLayer);
                    __instance.m_sectorIconSecond.BlinkIn(delay2);
                    delay2 += num1;
                }
                if (RundownManager.HasThirdLayer(__instance.m_data))
                {
                    SetupIcon(__instance.m_sectorIconThird, completionData.overloadCompletes > 0, LG_LayerType.ThirdLayer);
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
