namespace CustomLevelProgression.DataBlocks
{
    public class ExpeditionSettingsDataBlock : CustomDataBlock<ExpeditionSettingsDataBlock>
    {
        public ExpeditionCargoCageSettings CargoCage { get; set; }
        public GeneralExpeditionInfo Expedition { get; set; }
        public ExpeditionCompletionSettings CompletionSettings { get; set; }
    }
}