using AK;
using CustomLevelProgression.DataBlocks;
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
            var ev = Event;
            string typeName;
            object waveID = null;
            object populationID = null;
            object delay = null;
            object triggerAlarm = null;

            if (ev.Parameters.TryGetValue("WaveID", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("WaveID", out waveID);

                if (waveID != null)
                    waveID = Convert.ChangeType(waveID, type);
            }

            if (ev.Parameters.TryGetValue("PopulationID", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("PopulationID", out populationID);

                if (populationID != null)
                    populationID = Convert.ChangeType(populationID, type);
            }

            if (ev.Parameters.TryGetValue("Delay", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("Delay", out delay);

                if (delay != null)
                    delay = Convert.ChangeType(delay, type);
            }

            if (ev.Parameters.TryGetValue("TriggerAlarm", out typeName))
            {
                Type type = SearchForType(typeName);
                info.Parameters.TryGetValue("TriggerAlarm", out triggerAlarm);

                if (triggerAlarm != null)
                    triggerAlarm = Convert.ChangeType(triggerAlarm, type);
            }

            Activate(waveID, populationID, delay, triggerAlarm);
        }

        public void Activate(object waveID = null, object populationID = null, object delay = null, object triggerAlarm = null)
        {
            Activate(waveID == null ? 0U : (uint)waveID, populationID == null ? 0U : (uint)populationID, delay == null ? 0.0f : (float)delay, triggerAlarm == null ? false : (bool)triggerAlarm);
        }

        public void Activate(uint waveID, uint populationID, float delay, bool triggerAlarm)
        {
            GameInfo.StartCoroutine(ActivateSequence(waveID, populationID, delay, triggerAlarm), true);
        }

        private IEnumerator ActivateSequence(uint waveID, uint populationID, float delay, bool triggerAlarm)
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
                    Mastermind.Current.TriggerSurvivalWave(localPlayer.CourseNode, waveID, populationID, out ushort _, spawnDelay: 2f);
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
