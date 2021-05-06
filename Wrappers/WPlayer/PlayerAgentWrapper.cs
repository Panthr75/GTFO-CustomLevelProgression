using Agents;
using AIGraph;
using CullingSystem;
using CustomLevelProgression.Wrappers.WAgents;
using FX_EffectSystem;
using GameData;
using LevelGeneration;
using Player;
using SNetwork;
using UnhollowerBaseLib;
using Unity.Profiling;
using UnityEngine;

namespace CustomLevelProgression.Wrappers.WPlayer
{
    public class PlayerAgentWrapper : AgentWrapper
    {
        public new PlayerAgent WrappedObj => (PlayerAgent)base.obj;

        public PlayerAgentWrapper(PlayerAgent obj) : base(obj)
        { }

        public static int ANIM_LAYER_FULL_BODY { get => PlayerAgent.ANIM_LAYER_FULL_BODY; set => PlayerAgent.ANIM_LAYER_FULL_BODY = value; }
        public static int ANIM_LAYER_FPS_WEAPON_POSE { get => PlayerAgent.ANIM_LAYER_FPS_WEAPON_POSE; set => PlayerAgent.ANIM_LAYER_FPS_WEAPON_POSE = value; }
        public static int ANIM_LAYER_FPS_HAND_MOTION { get => PlayerAgent.ANIM_LAYER_FPS_HAND_MOTION; set => PlayerAgent.ANIM_LAYER_FPS_HAND_MOTION = value; }
        public static int ANIM_LAYER_3RD_UPPERBODY { get => PlayerAgent.ANIM_LAYER_3RD_UPPERBODY; set => PlayerAgent.ANIM_LAYER_3RD_UPPERBODY = value; }
        public static int ANIM_LAYER_LEFT_HAND { get => PlayerAgent.ANIM_LAYER_LEFT_HAND; set => PlayerAgent.ANIM_LAYER_LEFT_HAND = value; }
        public static int ANIM_LAYER_RIGHT_HAND { get => PlayerAgent.ANIM_LAYER_RIGHT_HAND; set => PlayerAgent.ANIM_LAYER_RIGHT_HAND = value; }
        public static ProfilerMarker s_perfMarkerUpdateLocal { get => PlayerAgent.s_perfMarkerUpdateLocal; set => PlayerAgent.s_perfMarkerUpdateLocal = value; }
        public static ProfilerMarker s_perfMarkerUpdateSync { get => PlayerAgent.s_perfMarkerUpdateSync; set => PlayerAgent.s_perfMarkerUpdateSync = value; }
        public static ProfilerMarker s_perfMarkerLateUpdate { get => PlayerAgent.s_perfMarkerLateUpdate; set => PlayerAgent.s_perfMarkerLateUpdate = value; }
        public static ProfilerMarker s_perfMarkerPingTargetRaycast { get => PlayerAgent.s_perfMarkerPingTargetRaycast; set => PlayerAgent.s_perfMarkerPingTargetRaycast = value; }
        public static float LastLocalShotFiredTime { get => PlayerAgent.LastLocalShotFiredTime; set => PlayerAgent.LastLocalShotFiredTime = value; }
        public float m_infectionLastDelta { get => this.WrappedObj.m_infectionLastDelta; set => this.WrappedObj.m_infectionLastDelta = value; }
        public float m_noiseTimestamp { get => this.WrappedObj.m_noiseTimestamp; set => this.WrappedObj.m_noiseTimestamp = value; }
        public Transform m_aimTarget { get => this.WrappedObj.m_aimTarget; set => this.WrappedObj.m_aimTarget = value; }
        public bool m_alive { get => this.WrappedObj.m_alive; set => this.WrappedObj.m_alive = value; }
        public Il2CppStructArray<DialogCharFilter> m_playerCharacters { get => this.WrappedObj.m_playerCharacters; set => m_playerCharacters = value; }
        public KeyCode m_ffKey1 { get => this.WrappedObj.m_ffKey1; set => this.WrappedObj.m_ffKey1 = value; }
        public KeyCode m_ffKey2 { get => this.WrappedObj.m_ffKey2; set => this.WrappedObj.m_ffKey2 = value; }
        //public global::InControl.InputControlType m_ffGamepadKey { get => this.player.m_ffGamepadKey; set => this.player.m_ffGamepadKey = value; }
        public float m_pingAgainTimer { get => this.WrappedObj.m_pingAgainTimer; set => this.WrappedObj.m_pingAgainTimer = value; }
        public bool m_teammatesVisible { get => this.WrappedObj.m_teammatesVisible; set => this.WrappedObj.m_teammatesVisible = value; }
        public Agent.NoiseType m_debugLastNoise { get => this.WrappedObj.m_debugLastNoise; set => this.WrappedObj.m_debugLastNoise = value; }
        public float m_debugLastNoiseChange { get => this.WrappedObj.m_debugLastNoiseChange; set => this.WrappedObj.m_debugLastNoiseChange = value; }
        public bool m_inited { get => this.WrappedObj.m_inited; set => this.WrappedObj.m_inited = value; }
        public float m_flashLightRange { get => this.WrappedObj.m_flashLightRange; set => this.WrappedObj.m_flashLightRange = value; }
        public float m_infectionNextUpdate { get => this.WrappedObj.m_infectionNextUpdate; set => this.WrappedObj.m_infectionNextUpdate = value; }
        public int m_cleanupAttackersStep { get => this.WrappedObj.m_cleanupAttackersStep; set => this.WrappedObj.m_cleanupAttackersStep = value; }
        public float m_attackerScore { get => this.WrappedObj.m_attackerScore; set => this.WrappedObj.m_attackerScore = value; }
        public Il2CppSystem.Collections.Generic.List<Agent> m_attackers { get => this.WrappedObj.m_attackers; set => this.WrappedObj.m_attackers = value; }
        public Vector3 m_pingPos { get => this.WrappedObj.m_pingPos; set => this.WrappedObj.m_pingPos = value; }
        public iPlayerPingTarget m_lastPingedTarget { get => this.WrappedObj.m_lastPingedTarget; set => this.WrappedObj.m_lastPingedTarget = value; }
        public iPlayerPingTarget m_pingTarget { get => this.WrappedObj.m_pingTarget; set => this.WrappedObj.m_pingTarget = value; }
        public float m_debugRain { get => this.WrappedObj.m_debugRain; set => this.WrappedObj.m_debugRain = value; }
        public float m_pingCheckTimer { get => this.WrappedObj.m_pingCheckTimer; set => this.WrappedObj.m_pingCheckTimer = value; }
        public Vector3 m_navigationPosition { get => this.WrappedObj.m_navigationPosition; set => this.WrappedObj.m_navigationPosition = value; }
        public Vector3 m_eyePosition { get => this.WrappedObj.m_eyePosition; set => this.WrappedObj.m_eyePosition = value; }
        public float m_sweatLimit { get => this.WrappedObj.m_sweatLimit; set => this.WrappedObj.m_sweatLimit = value; }
        public float m_infectionDamageHitreactTimer { get => this.WrappedObj.m_infectionDamageHitreactTimer; set => this.WrappedObj.m_infectionDamageHitreactTimer = value; }
        public float m_infectionBreathingTimer { get => this.WrappedObj.m_infectionBreathingTimer; set => this.WrappedObj.m_infectionBreathingTimer = value; }
        public float m_infectionSweatNextUpdate { get => this.WrappedObj.m_infectionSweatNextUpdate; set => this.WrappedObj.m_infectionSweatNextUpdate = value; }
        public Agent m_tempAttacker { get => this.WrappedObj.m_tempAttacker; set => this.WrappedObj.m_tempAttacker = value; }
        public Transform m_mapVisibilityTrans { get => this.WrappedObj.m_mapVisibilityTrans; set => this.WrappedObj.m_mapVisibilityTrans = value; }
        public bool m_wasDeSpawned { get => this.WrappedObj.m_wasDeSpawned; set => this.WrappedObj.m_wasDeSpawned = value; }
        public AIG_CourseNode m_courseNode { get => this.WrappedObj.m_courseNode; set => this.WrappedObj.m_courseNode = value; }
        public bool AllowMeleeStaminaRegen { get => this.WrappedObj.AllowMeleeStaminaRegen; set => this.WrappedObj.AllowMeleeStaminaRegen = value; }
        public bool IsBeingDestroyed { get => this.WrappedObj.IsBeingDestroyed; set => this.WrappedObj.IsBeingDestroyed = value; }
        public bool IsBeingDespawned { get => this.WrappedObj.IsBeingDespawned; set => this.WrappedObj.IsBeingDespawned = value; }
        public DialogCharFilter PlayerCharacterFilter { get => this.WrappedObj.PlayerCharacterFilter; }
        public float InfectionTargetHealth { get => this.WrappedObj.InfectionTargetHealth; }
        public float InfectionTargetHealthRel { get => this.WrappedObj.InfectionTargetHealthRel; }
        public Vector3 CamPos { get => this.WrappedObj.CamPos; }
        public bool IsEnabled { get => this.WrappedObj.IsEnabled; set => this.WrappedObj.IsEnabled = value; }
        public int CurrentFloorNr { get => this.WrappedObj.CurrentFloorNr; set => this.WrappedObj.CurrentFloorNr = value; }
        public float MeleeStaminaRel { get => this.WrappedObj.MeleeStaminaRel; set => this.WrappedObj.MeleeStaminaRel = value; }
        public PUI_RadarObject m_radarItem { get => this.WrappedObj.m_radarItem; set => this.WrappedObj.m_radarItem = value; }
        public float MeleeBuffTimer { get => this.WrappedObj.MeleeBuffTimer; set => this.WrappedObj.MeleeBuffTimer = value; }
        public PlayerSyncModelData PlayerSyncModel { get => this.WrappedObj.PlayerSyncModel; set => this.WrappedObj.PlayerSyncModel = value; }
        public LG_Zone m_lastEnteredZone { get => this.WrappedObj.m_lastEnteredZone; set => this.WrappedObj.m_lastEnteredZone = value; }
        public AIG_INode m_goodNode { get => this.WrappedObj.m_goodNode; set => this.WrappedObj.m_goodNode = value; }
        public bool m_hasFirstGoodNode { get => this.WrappedObj.m_hasFirstGoodNode; set => this.WrappedObj.m_hasFirstGoodNode = value; }
        public int m_currentFloorNR { get => this.WrappedObj.m_currentFloorNR; set => this.WrappedObj.m_currentFloorNR = value; }
        public Vector3 m_goodPosition { get => this.WrappedObj.m_goodPosition; set => this.WrappedObj.m_goodPosition = value; }
        public AIG_NodeCluster m_goodNodeCluster { get => this.WrappedObj.m_goodNodeCluster; set => this.WrappedObj.m_goodNodeCluster = value; }
        public AIG_NodeVolume m_goodNodeVolume { get => this.WrappedObj.m_goodNodeVolume; set => this.WrappedObj.m_goodNodeVolume = value; }
        public Vector3 m_tempNodeLookupPosition { get => this.WrappedObj.m_tempNodeLookupPosition; set => this.WrappedObj.m_tempNodeLookupPosition = value; }
        public float m_ev_lastModificationTime { get => this.WrappedObj.m_ev_lastModificationTime; set => this.WrappedObj.m_ev_lastModificationTime = value; }
        public float m_infectionModifySoundTimer { get => this.WrappedObj.m_infectionModifySoundTimer; set => this.WrappedObj.m_infectionModifySoundTimer = value; }
        public bool m_infectionModifySoundPlaying { get => this.WrappedObj.m_infectionModifySoundPlaying; set => this.WrappedObj.m_infectionModifySoundPlaying = value; }
        public SNet_Player Owner { get => this.WrappedObj.Owner; }
        public int PlayerSlotIndex { get => this.WrappedObj.PlayerSlotIndex; }
        public string PlayerName { get => this.WrappedObj.PlayerName; }
        public int CharacterID { get => this.WrappedObj.CharacterID; set => this.WrappedObj.CharacterID = value; }
        public Animator AnimatorBody { get => this.WrappedObj.AnimatorBody; set => this.WrappedObj.AnimatorBody = value; }
        public Animator AnimatorArms { get => this.WrappedObj.AnimatorArms; set => this.WrappedObj.AnimatorArms = value; }
        public PlayerSpawnpoint SpawnPoint { get => this.WrappedObj.SpawnPoint; set => this.WrappedObj.SpawnPoint = value; }
        public PlayerAnimationEvents AnimEvents { get => this.WrappedObj.AnimEvents; set => this.WrappedObj.AnimEvents = value; }
        public bool m_isSetup { get => this.WrappedObj.m_isSetup; set => this.WrappedObj.m_isSetup = value; }
        public EV_TargetData EffectVolumeTargetData { get => this.WrappedObj.EffectVolumeTargetData; }
        public float m_meleeStaminaRegenSpeed { get => this.WrappedObj.m_meleeStaminaRegenSpeed; set => this.WrappedObj.m_meleeStaminaRegenSpeed = value; }
        public float m_meleeStaminaRel { get => this.WrappedObj.m_meleeStaminaRel; set => this.WrappedObj.m_meleeStaminaRel = value; }
        public Transform m_FPSTentacleTarget { get => this.WrappedObj.m_FPSTentacleTarget; set => this.WrappedObj.m_FPSTentacleTarget = value; }
        public Vector3 m_camPos { get => this.WrappedObj.m_camPos; set => this.WrappedObj.m_camPos = value; }
        public bool IsNSpace { get => this.WrappedObj.IsNSpace; set => this.WrappedObj.IsNSpace = value; }
        public eFocusState InputFilter { get => this.WrappedObj.InputFilter; set => this.WrappedObj.InputFilter = value; }
        public PlayerSyncIK SyncIK { get => this.WrappedObj.SyncIK; set => this.WrappedObj.SyncIK = value; }
        public PlayerInventoryBase Inventory { get => this.WrappedObj.Inventory; set => this.WrappedObj.Inventory = value; }
        public FirstPersonItemHolder FPItemHolder { get => this.WrappedObj.FPItemHolder; set => this.WrappedObj.FPItemHolder = value; }
        public Dam_PlayerDamageBase Damage { get => this.WrappedObj.Damage; set => this.WrappedObj.Damage = value; }
        public PlayerParasites Parasites { get => this.WrappedObj.Parasites; set => this.WrappedObj.Parasites = value; }
        public PlaceNavMarkerOnGO NavMarker { get => this.WrappedObj.NavMarker; set => this.WrappedObj.NavMarker = value; }
        public Interact_Revive ReviveInteraction { get => this.WrappedObj.ReviveInteraction; set => this.WrappedObj.ReviveInteraction = value; }
        public PlayerInteraction Interaction { get => this.WrappedObj.Interaction; set => this.WrappedObj.Interaction = value; }
        public PlayerLocomotion Locomotion { get => this.WrappedObj.Locomotion; set => this.WrappedObj.Locomotion = value; }
        public PlayerBreathing Breathing { get => this.WrappedObj.Breathing; set => this.WrappedObj.Breathing = value; }
        public PlayerSync Sync { get => this.WrappedObj.Sync; set => this.WrappedObj.Sync = value; }
        public PlayerCharacterController PlayerCharacterController { get => this.WrappedObj.PlayerCharacterController; set => this.WrappedObj.PlayerCharacterController = value; }
        public float m_meleeStaminaLast { get => this.WrappedObj.m_meleeStaminaLast; set => this.WrappedObj.m_meleeStaminaLast = value; }
        public PlayerVoice Voice { get => this.WrappedObj.Voice; set => this.WrappedObj.Voice = value; }
        public CellSoundPlayer Sound { get => this.WrappedObj.Sound; set => this.WrappedObj.Sound = value; }
        public C_MovingCuller m_movingCuller { get => this.WrappedObj.m_movingCuller; set => this.WrappedObj.m_movingCuller = value; }
        public string InteractionName { get => this.WrappedObj.InteractionName; }
        public FX_PointLight m_ambientPoint { get => this.WrappedObj.m_ambientPoint; set => this.WrappedObj.m_ambientPoint = value; }
        public bool DeadDebugMode { get => this.WrappedObj.DeadDebugMode; set => this.WrappedObj.DeadDebugMode = value; }
        public PlayerDataBlock PlayerData { get => this.WrappedObj.PlayerData; set => this.WrappedObj.PlayerData = value; }
        public FPSCamera FPSCamera { get => this.WrappedObj.FPSCamera; set => this.WrappedObj.FPSCamera = value; }
        public Light m_ambienceLight { get => this.WrappedObj.m_ambienceLight; set => this.WrappedObj.m_ambienceLight = value; }
        public MapperObject m_mapObj { get => this.WrappedObj.m_mapObj; set => this.WrappedObj.m_mapObj = value; }
        public CameraController MapCamera { get => this.WrappedObj.MapCamera; set => this.WrappedObj.MapCamera = value; }
        public Il2CppReferenceArray<GameObject> m_modelsForSync { get => this.WrappedObj.m_modelsForSync; set => this.WrappedObj.m_modelsForSync = value; }
        public MapperCamera m_mapperCamera { get => this.WrappedObj.m_mapperCamera; set => this.WrappedObj.m_mapperCamera = value; }

        public static bool TrySetFirstNodeToAllPlayers() => PlayerAgent.TrySetFirstNodeToAllPlayers();
        public static bool TrySetFirstNodeToLocalPlayer() => PlayerAgent.TrySetFirstNodeToLocalPlayer();
        public void AquireAmbientPoint() => this.AquireAmbientPoint();
        public bool CheckDialogCondition(PlayerAgent sourceAgent, DialogCondition cond) => this.WrappedObj.CheckDialogCondition(sourceAgent, cond);
        public void CleanupAttackers() => this.WrappedObj.CleanupAttackers();
        public void Disable() => this.WrappedObj.Disable();
        public void Eject() => this.WrappedObj.Eject();
        public void Enable() => this.WrappedObj.Enable();
        public Il2CppSystem.Collections.Generic.List<Agent> GetAttackers() => this.WrappedObj.GetAttackers();
        public float GetAttackersScore() => this.WrappedObj.GetAttackersScore();
        public float GetDetectionMod(Vector3 dir, float distance) => this.WrappedObj.GetDetectionMod(dir, distance);
        public Transform GetHeadCamTransform() => this.WrappedObj.GetHeadCamTransform();
        public SNet_Replicator GetReplicator() => this.WrappedObj.GetReplicator();
        public void GiveAmmoRel(float ammoStandardRel, float ammoSpecialRel, float ammoClassRel) => this.WrappedObj.GiveAmmoRel(ammoStandardRel, ammoSpecialRel, ammoClassRel);
        public void GiveDisinfection(float amountRel) => this.WrappedObj.GiveDisinfection(amountRel);
        public void GiveHealth(float amountRel) => this.WrappedObj.GiveHealth(amountRel);
        public bool HasDetectionMod(out float distance) => this.WrappedObj.HasDetectionMod(out distance);
        public void InitManuallySpawned(int characterIndex) => this.WrappedObj.InitManuallySpawned(characterIndex);
        public void LateUpdate() => this.WrappedObj.LateUpdate();
        public void LevelCleanup() => this.WrappedObj.LevelCleanup();
        public bool NeedDisinfection() => this.WrappedObj.NeedDisinfection();
        public bool NeedHealth() => this.WrappedObj.NeedHealth();
        public bool NeedToolAmmo() => this.WrappedObj.NeedToolAmmo();
        public bool NeedWeaponAmmo() => this.WrappedObj.NeedWeaponAmmo();
        public void OnDespawn() => this.WrappedObj.OnDespawn();
        public void OnDestroy() => this.WrappedObj.OnDestroy();
        public void OnPlayerNameChanged() => this.WrappedObj.OnPlayerNameChanged();
        public void PositionHasBeenUpdated() => this.WrappedObj.PositionHasBeenUpdated();
        public void ReceiveModification(EV_ModificationData data, float lastModificationTime) => this.WrappedObj.ReceiveModification(data, lastModificationTime);
        public void RegisterAttacker(Agent attacker) => this.WrappedObj.RegisterAttacker(attacker);
        public void SetTeammateInfoVisible(bool value) => this.WrappedObj.SetTeammateInfoVisible(value);
        public void Setup(int characterID) => this.WrappedObj.Setup(characterID);
        public void TeleportTo(Vector3 pos) => this.WrappedObj.TeleportTo(pos);
        public void TriggerEnemySpottedDialog() => this.WrappedObj.TriggerEnemySpottedDialog();
        public void TriggerMarkerPing(iPlayerPingTarget target, Vector3 worldPos) => this.WrappedObj.TriggerMarkerPing(target, worldPos);
        public bool TryWarpBack() => this.WrappedObj.TryWarpBack();
        public bool TryWarpTo(Vector3 position, Vector3 lookDir) => this.WrappedObj.TryWarpTo(position, lookDir);
        public void UnregisterAttacker(Agent attacker) => this.WrappedObj.UnregisterAttacker(attacker);
        public void Update() => this.WrappedObj.Update();
        public void UpdateGlobalInput() => this.WrappedObj.UpdateGlobalInput();
        public void UpdateGoodNode(AIG_INode node) => this.WrappedObj.UpdateGoodNode(node);
        public void UpdateGoodNodeAndArea() => this.WrappedObj.UpdateGoodNodeAndArea();
        public void UpdateInfectionLocal() => this.WrappedObj.UpdateInfectionLocal();
        public void WarpBack(Vector3 pos, AIG_CourseNode cNode) => this.WrappedObj.WarpBack(pos, cNode);
        public void WarpTo(Vector3 position, Quaternion rotation, pPlayerLocationData locationData) => this.WrappedObj.WarpTo(position, rotation, locationData);
        public void WarpTo(pPlayerWarpData warpData) => this.WrappedObj.WarpTo(warpData);
    }
}
