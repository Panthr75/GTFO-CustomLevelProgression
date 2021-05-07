using AK;
using GameData;
using HarmonyLib;
using LevelGeneration;
using UnityEngine;

namespace CustomLevelProgression.Patches.LevelGen
{
    public class LG_WardenObjective_ReactorPatch : CustomPatch
    {
        public LG_WardenObjective_ReactorPatch(Harmony harmony) : base(harmony, PatchType.Prefix, typeof(LG_WardenObjective_Reactor), nameof(LG_WardenObjective_Reactor.Update))
        { }

        public static bool Invoke(LG_WardenObjective_Reactor __instance)
        {
            float num1 = (1f - __instance.m_currentWaveProgress) * __instance.m_currentDuration;
            switch (__instance.m_currentState.status)
            {
                case eReactorStatus.Active_Idle:
                    __instance.SetGUIMessage(false);
                    break;
                case eReactorStatus.Startup_intro:
                    __instance.SetGUIMessage(true, "REACTOR STARTUP TEST (" + (object)__instance.m_currentWaveCount + " of " + (object)__instance.m_waveCountMax + ") WARMING UP.. ", ePUIMessageStyle.Default);
                    int num2 = (int)__instance.m_sound.SetRTPCValue(GAME_PARAMETERS.REACTOR_POWER, __instance.m_currentWaveProgress * 100f);
                    if (!__instance.m_alarmCountdownPlayed && (double)num1 <= 10.0)
                    {
                        int num3 = (int)__instance.m_sound.Post(EVENTS.REACTOR_ALARM_COUNTDOWN);
                        __instance.m_alarmCountdownPlayed = true;
                    }
                    if (!WardenObjectiveManager.ActiveWardenObjective(__instance.SpawnNode.LayerType).LightsOnDuringIntro)
                    {
                        float num3 = (float)((double)__instance.m_currentWaveProgress * (double)__instance.m_currentWaveProgress * 0.400000005960464 + 0.0500000007450581);
                        __instance.UpdateLightCollection(true, true, true, 32f, 2f, num3, num3);
                        break;
                    }
                    break;
                case eReactorStatus.Startup_intense:
                    __instance.SetGUIMessage(true, "REACTOR PERFORMING HIGH INTENSITY TEST (" + (object)__instance.m_currentWaveCount + "/" + (object)__instance.m_waveCountMax + ")");
                    __instance.UpdateLightCollection(false, true, true, 32f, 2f);
                    if (!__instance.m_alarmCountdownPlayed && (double)num1 <= 10.0)
                    {
                        int num3 = (int)__instance.m_sound.Post(EVENTS.REACTOR_ALARM_WARNING_3);
                        __instance.m_alarmCountdownPlayed = true;
                        break;
                    }
                    break;
                case eReactorStatus.Startup_waitForVerify:
                    if (__instance.m_currentWaveData.HasVerificationTerminal)
                        __instance.SetGUIMessage(true, "VERIFICATION (" + (object)__instance.m_currentWaveCount + "/" + (object)__instance.m_waveCountMax + "). CODE REQUIRED FOR <color=orange>REACTOR_VERIFY</color> LOCATED IN LOG FILE IN <color=orange>" + __instance.m_currentWaveData.VerificationTerminalSerial + "</color>", ePUIMessageStyle.Warning, timerPrefix: "<size=125%>TIME UNTIL RESET: ", timerSuffix: "</size>");
                    else
                        __instance.SetGUIMessage(true, "SECURITY VERIFICATION (" + (object)__instance.m_currentWaveCount + "/" + (object)__instance.m_waveCountMax + "). USE COMMAND <color=orange>REACTOR_VERIFY</color> AND CODE <color=orange>" + __instance.CurrentStateOverrideCode + "</color> BEFORE SAFETY RESET!", ePUIMessageStyle.Warning, timerPrefix: "<size=125%>TIME UNTIL RESET: ", timerSuffix: "</size>");
                    __instance.m_lightCollection.UpdateRandomMode(100f);
                    break;
                case eReactorStatus.Startup_complete:
                    __instance.SetGUIMessage(true, "REACTOR STARTUP SEQUENCE COMPLETED", ePUIMessageStyle.Default, false);
                    if (SNetwork.SNet.IsMaster && (double)Clock.Time > (double)__instance.m_objectiveCompleteTimer)
                    {
                        __instance.AttemptInteract(eReactorInteraction.Goto_active);
                        WardenObjectiveManager.OnLocalPlayerSolvedObjectiveItem(__instance.SpawnNode.LayerType, __instance.TryCast<iWardenObjectiveItem>());
                        break;
                    }
                    break;
                case eReactorStatus.Shutdown_waitForVerify:
                    __instance.SetGUIMessage(true, "SECURITY VERIFICATION REQUIRED. USE COMMAND <color=orange>REACTOR_VERIFY</color> AND CODE <color=orange>" + __instance.CurrentStateOverrideCode + "</color> TO VERIFY.", ePUIMessageStyle.Warning, false);
                    break;
                case eReactorStatus.Shutdown_puzzleChaos:
                    __instance.SetGUIMessage(false);
                    break;
                case eReactorStatus.Shutdown_complete:
                    if ((double)Clock.Time > (double)__instance.m_objectiveCompleteTimer)
                    {
                        if (!__instance.m_shutdownCompleteShown)
                        {
                            GuiManager.PlayerLayer.m_wardenIntel.ShowSubObjectiveMessage("", "REACTOR SHUTDOWN SEQUENCE COMPLETED");
                            __instance.m_shutdownCompleteShown = true;
                        }
                        if (SNetwork.SNet.IsMaster)
                        {
                            __instance.AttemptInteract(eReactorInteraction.Goto_inactive);
                            WardenObjectiveManager.OnLocalPlayerSolvedObjectiveItem(__instance.SpawnNode.LayerType, __instance.TryCast<iWardenObjectiveItem>());
                            break;
                        }
                        break;
                    }
                    break;
            }
            if (!SNetwork.SNet.IsMaster)
                return false;

            if (__instance.m_spawnEnemies && __instance.m_currentEnemyWaveIndex < __instance.m_currentWaveData.EnemyWaves.Count)
            {
                ReactorWaveEnemyData enemyWave = __instance.m_currentWaveData.EnemyWaves[__instance.m_currentEnemyWaveIndex];
                if (Mastermind.Current.TryGetEvent(__instance.m_enemyWaveID, out Mastermind.MastermindEvent masterMindEvent))
                {
                    masterMindEvent.StopEvent();
                }

                if ((double)__instance.m_currentWaveProgress > (double)enemyWave.SpawnTimeRel)
                {
                    Debug.Log(Deb.Enemy("Reactor Spawning wave at " + (object)enemyWave.SpawnTimeRel + " with type: " + (object)enemyWave.SpawnType));
                    ushort m_enemyWaveID;
                    switch (enemyWave.SpawnType)
                    {
                        case eReactorWaveSpawnType.ClosestToReactorNoPlayerBetween:
                            if (Mastermind.Current.TriggerSurvivalWave(__instance.m_reactorArea.m_courseNode, enemyWave.WaveSettings, enemyWave.WavePopulation, out m_enemyWaveID, SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers))
                            {
                                __instance.m_enemyWaveID = m_enemyWaveID;
                            }
                            else
                            {
                                __instance.m_enemyWaveID = 0;
                            }
                            ++__instance.m_currentEnemyWaveIndex;
                            break;
                        case eReactorWaveSpawnType.InElevatorZone:
                            if (Mastermind.Current.TriggerSurvivalWave(Builder.GetElevatorZone().m_courseNodes[0], enemyWave.WaveSettings, enemyWave.WavePopulation, out m_enemyWaveID, SurvivalWaveSpawnType.InSuppliedCourseNodeZone))
                            {
                                __instance.m_enemyWaveID = m_enemyWaveID;
                            }
                            else
                            {
                                __instance.m_enemyWaveID = 0;
                            }
                            ++__instance.m_currentEnemyWaveIndex;
                            break;
                    }
                }
            }
            if (!__instance.m_progressUpdateEnabled)
                return false;

            __instance.m_currentWaveProgress += Clock.Delta / __instance.m_currentDuration;
            __instance.AttemptInteract(eReactorInteraction.Update_stateProgress, __instance.m_currentWaveProgress);
            if ((double)__instance.m_currentWaveProgress < 1.0)
                return false;

            Debug.Log("MASTER, startupStateProgress done, current status: " + __instance.m_currentState.status + " currennt count: " + __instance.m_currentWaveCount);
            switch (__instance.m_currentState.status)
            {
                case eReactorStatus.Startup_intro:
                    __instance.AttemptInteract(eReactorInteraction.Intensify_startup, __instance.m_currentWaveProgress);
                    break;
                case eReactorStatus.Startup_intense:
                    __instance.AttemptInteract(eReactorInteraction.WaitForVerify_startup, __instance.m_currentWaveProgress);
                    break;
                case eReactorStatus.Startup_waitForVerify:
                    __instance.AttemptInteract(eReactorInteraction.Verify_fail, __instance.m_currentWaveProgress);
                    break;
                case eReactorStatus.Shutdown_intro:
                    __instance.AttemptInteract(eReactorInteraction.WaitForVerify_shutdown, __instance.m_currentWaveProgress);
                    break;
                default:
                    Debug.LogError("MASTER, Reactor m_startupStateProgress reached 1 in a state withouth action, status: " + __instance.m_currentState.status);
                    break;
            }
            return false;
        }
    }
}
