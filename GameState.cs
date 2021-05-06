namespace CustomLevelProgression
{
    public enum GameState
    {
        None,

        Startup,
        Offline,

        NoLobby,
        InLobby,

        Generating,
        ReadyToStopElevatorRide,
        StopElevatorRide,
        InLevel,

        ExpeditionFailed,
        ExpeditionSuccess
    }
}