using AK;
using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Parsers;
using CustomLevelProgression.Utilities;
using GameData;
using System;
using System.Collections;
using UnityEngine;

namespace CustomLevelProgression.CustomEvents
{
    public class ActivateSurvivalWaveEvent : CustomEvent
    {
        public ActivateSurvivalWaveEvent() : base(6U)
        { }

        public override void Activate(EventInfo info)
        {
            Log.Message("Activate ActivateSurvivalWaveEvent");

            string waveID;
            string populationID;
            string delay;
            string triggerAlarm;

            info.Parameters.TryGetValue("SettingsID", out waveID);
            info.Parameters.TryGetValue("PopulationID", out populationID);
            info.Parameters.TryGetValue("Delay", out delay);
            info.Parameters.TryGetValue("TriggerAlarm", out triggerAlarm);

            Activate(waveID, populationID, delay, triggerAlarm);
        }

        public void Activate(string settingsID = null, string populationID = null, string delay = null, string triggerAlarm = null)
        {
            Activate(DataBlockIDParser<SurvivalWaveSettingsDataBlock>.Parse(settingsID), DataBlockIDParser<SurvivalWaveSettingsDataBlock>.Parse(populationID), FloatParser.Parse(delay), BooleanParser.Parse(triggerAlarm));
        }

        public void Activate(uint settingsID, uint populationID, float delay, bool triggerAlarm)
        {
            GameInfo.StartCoroutine(ActivateSequence(settingsID, populationID, delay, triggerAlarm), true);
        }

        private IEnumerator ActivateSequence(uint settingsID, uint populationID, float delay, bool triggerAlarm)
        {
            var localPlayer = ExtendedPlayerAgent.LocalPlayer;
            if (localPlayer == null)
            {
                Debug.LogError("Error: No local player found to use as a reference node");
            }
            else
            {
                yield return new WaitForSeconds(delay);

                if (localPlayer.Owner.IsMaster)
                {
                    Mastermind.Current.TriggerSurvivalWave(localPlayer.CourseNode, settingsID, populationID, out ushort _, spawnDelay: 2f);
                }

                if (triggerAlarm)
                {
                    var manager = WardenObjectiveManager.Current;
                    manager.m_sound.UpdatePosition(localPlayer.CourseNode.Position);
                    manager.m_sound.Post(EVENTS.APEX_PUZZLE_START_ALARM);
                }
            }
        }
    }
}
