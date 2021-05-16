using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;
using GameData;
using LevelGeneration;
using System;

namespace CustomLevelProgression.CustomEvents
{
    public class OpenSecurityDoorEvent : CustomEvent
    {
        public OpenSecurityDoorEvent() : base(1U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate OpenSecurityDoorEvent");

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
            GameInfo.OpenSecurityDoor(layer, zoneIndex, buildFromIndex);
        }
    }
}