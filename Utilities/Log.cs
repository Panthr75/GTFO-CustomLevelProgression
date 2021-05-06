using BepInEx.Logging;

namespace CustomLevelProgression.Utilities
{
    public static class Log
    {
        private static readonly ManualLogSource logger = new ManualLogSource("LevelProgression");

        static Log() => Logger.Sources.Add(Log.logger);

        public static void Verbose(object msg) => Log.logger.LogInfo(msg);

        public static void Debug(object msg) => Log.logger.LogDebug(msg);

        public static void Message(object msg) => Log.logger.LogMessage(msg);

        public static void Error(object msg) => Log.logger.LogError(msg);

        public static void Warn(object msg) => Log.logger.LogWarning(msg);
    }
}
