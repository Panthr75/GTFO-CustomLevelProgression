using CustomLevelProgression.CustomEvents;
using CustomLevelProgression.DataBlocks;
using CustomLevelProgression.Networking;
using CustomLevelProgression.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLevelProgression
{
    public static class EventSequenceManager
    {
        private static Dictionary<uint, CustomEvent> events;

        private static IEnumerator SequenceRoutine(EventSequenceDataBlock sequence)
        {
            if (sequence.internalEnabled)
            {
                for (int index = 0; index < sequence.Events.Count; index++)
                {
                    var eventInfo = sequence.Events[index];
                    yield return EventRoutine(eventInfo);
                    yield return new WaitForSeconds(eventInfo.NextEventDelay);
                }
            }
        }

        private static IEnumerator EventRoutine(EventInfo eventInfo)
        {
            bool isMaster = NET.IsMaster;
            bool skip = (eventInfo.SkipIfClient && !isMaster) || (eventInfo.SkipIfMaster && isMaster);
            if (!skip && events.TryGetValue(eventInfo.EventID, out CustomEvent customEvent))
            {
                if (!eventInfo.RequiresProgression || GameInfo.MeetsProgressionRequirements(eventInfo.RequiredProgression))
                {
                    yield return new WaitForSeconds(eventInfo.ActivationDelay);

                    customEvent.Activate(eventInfo);
                }
            }
        }

        public static void StartSequence(uint blockID)
        {
            var sequence = EventSequenceDataBlock.GetBlock(blockID);
            if (sequence != null)
            {
                GameInfo.StartCoroutine(SequenceRoutine(sequence), true);
            }
        }

        public static void AddEvent(CustomEvent customEvent)
        {
            if (customEvent != null && !events.ContainsKey(customEvent.ID))
            {
                events.Add(customEvent.ID, customEvent);
            }
        }

        static EventSequenceManager()
        {
            // Create events dictionary
            events = new Dictionary<uint, CustomEvent>();

            // Add base events
            AddEvent(new OpenSecurityDoorEvent());
            AddEvent(new UnlockSecurityDoorEvent());
            AddEvent(new FogTransitionEvent());
            AddEvent(new WardenIntelEvent());
            AddEvent(new LightSequenceEvent());
            AddEvent(new ActivateSurvivalWaveEvent());
            AddEvent(new SoundEvent());
            AddEvent(new EventSequenceEvent());
        }
    }
}
