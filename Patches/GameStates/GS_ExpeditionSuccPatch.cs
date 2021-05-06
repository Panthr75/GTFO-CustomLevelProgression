using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_ExpeditionSuccPatch : CustomPatch
    {
        public GS_ExpeditionSuccPatch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(GS_ExpeditionSuccess), nameof(GS_ExpeditionSuccess.Enter))
        { }

        public static void Invoke()
        {
            var state = WardenObjectiveManager.CurrentState;
            var exp = RundownManager.GetActiveExpeditionData();
            var completionData = CompletionData.LoadFromCache();
            var data = completionData.GetData(exp.tier, exp.expeditionIndex);

            bool pe = true;
            if (state.main_status == eWardenObjectiveStatus.WardenObjectiveItemSolved)
            {
                data.highCompletes++;
            }
            else
            {
                pe = false;
            }

            if (state.second_status == eWardenObjectiveStatus.WardenObjectiveItemSolved)
            {
                data.extremeCompletes++;
            }
            else
            {
                pe = false;
            }

            if (state.third_status == eWardenObjectiveStatus.WardenObjectiveItemSolved)
            {
                data.overloadCompletes++;
            }
            else
            {
                pe = false;
            }

            if (pe)
            {
                data.peCompletes++;
            }

            completionData.Save();
        }
    }
}
