using CustomLevelProgression.Networking;
using HarmonyLib;
using SNetwork;

namespace CustomLevelProgression.Patches.SNet
{
    public class SNet_ReplicationPatch : CustomPatch
    {
        public SNet_ReplicationPatch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(SNet_Replication), nameof(SNet_Replication.RecieveBytes))
        { }

        public static bool Invoke(UnhollowerBaseLib.Il2CppStructArray<byte> bytes)
        {
            return !NET.ReceiveBytes(bytes);
        }
    }
}
