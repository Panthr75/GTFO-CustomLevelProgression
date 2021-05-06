using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_LobbyPatch : CustomPatch
    {
        public GS_LobbyPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_Lobby), nameof(GS_Lobby.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.InLobby;
        }
    }
}
