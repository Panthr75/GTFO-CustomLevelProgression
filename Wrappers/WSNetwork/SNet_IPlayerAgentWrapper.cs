using SNetwork;

namespace CustomLevelProgression.Wrappers.WSNetwork
{
    public interface SNet_IPlayerAgentWrapper
    {
        SNet_Replicator GetReplicator();

        void Eject();

        void OnDespawn();

        void OnPlayerNameChanged();
    }
}