using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using Enemies;
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
            var ev = Event;
            string typeName;
            object enemyID = null;
            object count = null;

            if (ev.Parameters.TryGetValue("EnemyID", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("EnemyID", out enemyID);

                if (enemyID != null)
                    enemyID = Convert.ChangeType(enemyID, type);
            }

            if (ev.Parameters.TryGetValue("Count", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("Count", out enemyID);

                if (count != null)
                    count = Convert.ChangeType(count, type);
            }

            Activate(enemyID, count);
        }

        public void Activate(object enemyID = null, object count = null)
        {
            Activate(enemyID == null ? 0U : (uint)enemyID, count == null ? 0 : (int)count);
        }

        public void Activate(uint enemyID, int count)
        {
            var enemies = UnityEngine.Object.FindObjectsOfType<EnemyAgent>();
            int curCount = 0;

            for (int index = 0; index < enemies.Count && (count == -1 || curCount < count); index++)
            {
                var enemy = enemies[index];
                if (enemy.EnemyData.persistentID == enemyID)
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