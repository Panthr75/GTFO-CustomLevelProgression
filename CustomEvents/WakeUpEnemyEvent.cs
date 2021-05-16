using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;
using Enemies;
using GameData;
using LevelGeneration;
using System;
using System.Collections;

namespace CustomLevelProgression.CustomEvents
{
    public class WakeUpEnemyEvent : CustomEvent
    {
        public WakeUpEnemyEvent() : base(9U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate WakeUpEnemyEvent");

            string enemyID;
            string count;
            string layer;
            string zoneIndex;
            string areaName;

            info.Parameters.TryGetValue("EnemyID", out enemyID);
            info.Parameters.TryGetValue("Count", out count);
            info.Parameters.TryGetValue("Layer", out layer);
            info.Parameters.TryGetValue("ZoneIndex", out zoneIndex);
            info.Parameters.TryGetValue("AreaName", out areaName);

            Activate(enemyID, count, layer, zoneIndex, areaName);
        }

        public void Activate(string enemyID = null, string count = null, string layer = null, string zoneIndex = null, string areaName = null)
        {
            Activate(DataBlockIDParser<EnemyDataBlock>.Parse(enemyID), Int32Parser.Parse(count), LG_LayerTypeParser.Parse(layer), eLocalZoneIndexParser.Parse(zoneIndex), areaName == null ? "a" : areaName.ToLower());
        }

        public void Activate(uint enemyID, int count, LG_LayerType layer, eLocalZoneIndex zoneIndex, string areaName)
        {
            var enemies = UnityEngine.Object.FindObjectsOfType<EnemyAgent>();
            int curCount = 0;

            for (int index = 0; index < enemies.Count && (count == -1 || curCount < count); index++)
            {
                var enemy = enemies[index];

                var node = enemy.CourseNode;
                if (node != null && node.LayerType == layer && node.m_zone.LocalIndex == zoneIndex && node.m_area.m_navInfo.Suffix.ToLower() == areaName)
                {
                    if (enemy.EnemyData.persistentID == enemyID && enemy.AI != null && enemy.Alive)
                    {
                        NM_NoiseData data = new NM_NoiseData()
                        {
                            noiseMaker = null,
                            position = enemy.Position,
                            radiusMin = 0f,
                            radiusMax = 100f,
                            yScale = 1f,
                            node = enemy.CourseNode,
                            type = NM_NoiseType.InstaDetect,
                            includeToNeightbourAreas = true,
                            raycastFirstNode = false
                        };

                        NoiseManager.MakeNoise(data);
                        curCount++;
                    }
                }
            }
        }
    }
}