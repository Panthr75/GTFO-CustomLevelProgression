using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using GameData;
using LevelGeneration;

namespace CustomLevelProgression.CustomEvents
{
    public class UnlockSecurityDoorEvent : CustomEvent
    {
        public UnlockSecurityDoorEvent() : base(2U)
        { }

        public override void Activate(EventInfo info)
        {
            string layer;
            string zoneIndex;
            string buildFromIndex;

            info.Parameters.TryGetValue("Layer", out layer);
            info.Parameters.TryGetValue("ZoneIndex", out zoneIndex);
            info.Parameters.TryGetValue("BuildFromIndex", out buildFromIndex);

            Activate(layer, zoneIndex, buildFromIndex);
        }

        public void Activate(string layer = null, string zoneIndex = null, string buildFromIndex = null)
        {
            Activate(LG_LayerTypeParser.Parse(layer), eLocalZoneIndexParser.Parse(zoneIndex), eLocalZoneIndexParser.Parse(buildFromIndex));
        }

        public void Activate(LG_LayerType layer, eLocalZoneIndex zoneIndex, eLocalZoneIndex buildFromIndex)
        {
            GameInfo.UnlockSecurityDoor(layer, zoneIndex, buildFromIndex);
        }
    }
}