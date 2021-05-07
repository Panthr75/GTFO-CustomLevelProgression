namespace CustomLevelProgression.Wrappers
{
    public interface IEffectVolumeTargetWrapper
    {
        EV_TargetData EffectVolumeTargetData { get; }

        void ReceiveModification(EV_ModificationData data, float lastModificationTime);
    }
}