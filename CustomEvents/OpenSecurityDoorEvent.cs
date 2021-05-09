using CustomLevelProgression.DataBlocks;
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

            var ev = Event;
            string typeName;
            object layer = null;
            object zoneIndex = null;
            object buildFromIndex = null;

            if (ev.Parameters.TryGetValue("Layer", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("Layer", out layer);

                if (layer != null)
                    layer = (LG_LayerType)Convert.ChangeType(layer, type);
            }

            if (ev.Parameters.TryGetValue("ZoneIndex", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("ZoneIndex", out zoneIndex);

                if (zoneIndex != null)
                    zoneIndex = (eLocalZoneIndex)Convert.ChangeType(zoneIndex, type);
            }

            if (ev.Parameters.TryGetValue("BuildFromIndex", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("BuildFromIndex", out buildFromIndex);

                if (buildFromIndex != null)
                    buildFromIndex = (eLocalZoneIndex)Convert.ChangeType(buildFromIndex, type);
            }

            Activate(layer, zoneIndex, buildFromIndex);
        }

        public void Activate(object layer = null, object zoneIndex = null, object buildFromIndex = null)
        {
            Activate(layer == null ? LG_LayerType.MainLayer : (LG_LayerType)layer, zoneIndex == null ? eLocalZoneIndex.Zone_0 : (eLocalZoneIndex)zoneIndex, buildFromIndex == null ? eLocalZoneIndex.Zone_0 : (eLocalZoneIndex)buildFromIndex);
        }

        public void Activate(LG_LayerType layer, eLocalZoneIndex zoneIndex, eLocalZoneIndex buildFromIndex)
        {
            var door = GameInfo.GetSecurityDoor(layer, zoneIndex, buildFromIndex);
            door?.m_sync.AttemptDoorInteraction(eDoorInteractionType.Open);
        }
    }
}