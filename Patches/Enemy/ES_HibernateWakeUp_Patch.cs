using HarmonyLib;
using Enemies;
using CustomLevelProgression.DataBlocks;

namespace CustomLevelProgression.Patches.Enemy
{
    public class ES_HibernateWakeUp_Patch : CustomPatch
    {
        public ES_HibernateWakeUp_Patch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(ES_HibernateWakeUp), nameof(ES_HibernateWakeUp.Enter))
        { }

        public static void Invoke(ES_HibernateWakeUp __instance)
        {
            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();
            foreach (var block in blocks)
            {
                if (block.Type == EventListenerType.EnemyWakeUp)
                {
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            var enemy = __instance.m_enemyAgent;
                            if (block.EnemyWakeup_EnemyID == enemy.EnemyDataID)
                            {
                                var lg_area = enemy.CourseNode.m_area;
                                var area = lg_area.m_navInfo.Suffix.ToLower();

                                var lg_zone = lg_area.m_zone;
                                var zone = lg_zone.LocalIndex;

                                var lg_layer = lg_zone.Layer;
                                var layer = lg_layer.m_type;

                                bool valid = block.EnemyWakeUp_ForLayers.Count == 0;
                                foreach (var validLayer in block.EnemyWakeUp_ForLayers)
                                {
                                    if (validLayer.Layer == layer)
                                    {
                                        valid = validLayer.Whitelist;
                                        foreach (var validZone in validLayer.Zones)
                                        {
                                            if (validZone.ZoneIndex == zone)
                                            {
                                                valid = validZone.Whitelist;
                                                foreach (var validArea in validZone.Areas)
                                                {
                                                    if (validArea.AreaName.ToLower() == area)
                                                    {
                                                        valid = validArea.Whitelist;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (valid)
                                {
                                    EventSequenceManager.StartSequence(block.EventSequenceOnActivate);
                                }
                            }
                            break;
                        }
                    }

                }
            }
        }
    }
}
