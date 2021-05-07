using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Utilities;
using HarmonyLib;

namespace CustomLevelProgression.Patches.GameStates
{
    public class GS_InLevelPatch : CustomPatch
    {
        public GS_InLevelPatch(Harmony harmony) : base(harmony, PatchType.Postfix, typeof(GS_InLevel), nameof(GS_InLevel.Enter))
        { }

        public static void Invoke()
        {
            GameInfo.State = GameState.InLevel;

            foreach (var light in UnityEngine.Object.FindObjectsOfType<LevelGeneration.LG_Light>())
            {
                //var area = light.GetC_Light()?.m_sourceNode?.CourseNode?.m_area;
                //Log.Message($"Found light, area: {area?.m_navInfo?.Suffix}, zone: {area?.m_zone?.Alias}, availableInLevel: {light.AvailableInLevel}");

                //if (area != null && light.AvailableInLevel)
                //{
                //    var controller = light.gameObject.GetComponent<Lights.LightController>();
                //    if (controller == null)
                //        controller = light.gameObject.AddComponent<Lights.LightController>();

                //    var a = controller.Area;
                //    if (a?.m_navInfo?.Suffix?.ToLower() != "a" || (a?.m_zone?.LocalIndex ?? GameData.eLocalZoneIndex.Zone_0) != GameData.eLocalZoneIndex.Zone_0 || (a?.m_zone?.Layer?.m_type ?? LevelGeneration.LG_LayerType.MainLayer) != LevelGeneration.LG_LayerType.MainLayer)
                //        controller.SetCurrentIntensity(0f);
                //}

                if (light.AvailableInLevel)
                {
                    if (light.gameObject.GetComponent<Lights.LightController>() == null)
                        light.gameObject.AddComponent<Lights.LightController>();
                }

            }

            var blocks = EventListenerDataBlock.GetAllBlocks();
            var exp = RundownManager.GetActiveExpeditionData();
            foreach (var block in blocks)
            {
                if (block.Type == EventListenerType.LevelLoad)
                {
                    foreach (var expedition in block.ForExpeditions)
                    {
                        if (expedition.ExpeditionIndex == exp.expeditionIndex && expedition.Tier == exp.tier)
                        {
                            Log.Message($"Start Level Load Event Sequence for listener id {block.persistentID} ({block.name})!");
                            if (block.LevelLoad_IncludeInitialDrop)
                            {
                                Log.Message("Peforming event sequence...");
                                EventSequenceManager.StartSequence(block.EventSequenceOnActivate);
                            }
                            break;
                        }
                    }
                    
                }
            }
        }
    }
}
