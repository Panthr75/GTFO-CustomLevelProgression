using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_FakeLobbyPatch : CustomPatch
    {
        public GS_FakeLobbyPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_FakeLobby), nameof(GS_FakeLobby.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.InLobby;
        }
    }
}
