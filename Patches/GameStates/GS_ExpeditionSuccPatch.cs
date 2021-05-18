using CustomLevelProgression.DataBlocks;
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

            bool highSolved = WardenObjectiveManager.CurrentState.main_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;
            bool extremeSolved = RundownManager.HasSecondaryLayer(RundownManager.ActiveExpedition) && WardenObjectiveManager.CurrentState.second_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;
            bool overloadSolved = RundownManager.HasThirdLayer(RundownManager.ActiveExpedition) && WardenObjectiveManager.CurrentState.third_status == eWardenObjectiveStatus.WardenObjectiveItemSolved;

            var progressionData = ExtendedPlayerAgent.LocalPlayer.ProgressionData;
            var expeditionSettingsBlocks = ExpeditionSettingsDataBlock.GetAllBlocks();
            foreach (var expeditionSettingsBlock in expeditionSettingsBlocks)
            {
                if (expeditionSettingsBlock.Expedition.ExpeditionIndex == exp.expeditionIndex && expeditionSettingsBlock.Expedition.Tier == exp.tier)
                {
                    var completionSettings = expeditionSettingsBlock.CompletionSettings;
                    foreach (var progressionUpdate in completionSettings.ProgressionUpdates)
                    {
                        if (GameInfo.MeetsProgressionRequirements(progressionUpdate.ProgressionRequirements))
                        {
                            if (progressionUpdate.HighCompleted == highSolved && progressionUpdate.ExtremeCompleted == extremeSolved && progressionUpdate.OverloadCompleted == overloadSolved)
                            {
                                progressionData.SetProgressionValue(exp.tier, exp.expeditionIndex, progressionUpdate.Value);
                            }
                        }
                    }
                }
            }

            progressionData.Save();
        }
    }
}
