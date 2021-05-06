using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using GameData;
using LevelGeneration;
using System;

namespace CustomLevelProgression.CustomEvents
{
    public class UnlockSecurityDoorEvent : CustomEvent
    {
        public UnlockSecurityDoorEvent() : base(2U)
        { }

        public override void Activate(EventInfo info)
        {
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
                    layer = Convert.ChangeType(layer, type);
            }

            if (ev.Parameters.TryGetValue("ZoneIndex", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("ZoneIndex", out zoneIndex);

                if (zoneIndex != null)
                    zoneIndex = Convert.ChangeType(zoneIndex, type);
            }

            if (ev.Parameters.TryGetValue("BuildFromIndex", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("BuildFromIndex", out buildFromIndex);

                if (buildFromIndex != null)
                    buildFromIndex = Convert.ChangeType(buildFromIndex, type);
            }

            Activate(layer, zoneIndex, buildFromIndex);
        }

        public void Activate(object layer = null, object zoneIndex = null, object buildFromIndex = null)
        {
            Activate(layer == null ? LG_LayerType.MainLayer : (LG_LayerType)(byte)layer, zoneIndex == null ? eLocalZoneIndex.Zone_0 : (eLocalZoneIndex)(byte)zoneIndex, buildFromIndex == null ? eLocalZoneIndex.Zone_0 : (eLocalZoneIndex)(byte)buildFromIndex);
        }

        public void Activate(LG_LayerType layer, eLocalZoneIndex zoneIndex, eLocalZoneIndex buildFromIndex)
        {
            var door = GameInfo.GetSecurityDoor(layer, zoneIndex, buildFromIndex);
            GameInfo.UnlockSecurityDoor(door);
        }
    }
}