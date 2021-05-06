using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_NoLobbyPatch : CustomPatch
    {
        public GS_NoLobbyPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_NoLobby), nameof(GS_NoLobby.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.NoLobby;
        }
    }
}
