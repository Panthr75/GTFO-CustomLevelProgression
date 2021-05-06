using Agents;
using AIGraph;
using Detection;
using SNetwork;
using UnityEngine;
using CustomLevelProgression.Wrappers.WUnityEngine;

namespace CustomLevelProgression.Wrappers.WAgents
{
    public class AgentWrapper : MonoBehaviourWrapper
    {
        public new Agent WrappedObj => (Agent)base.obj;

        public AgentWrapper(Agent obj) : base(obj)
        { }

        public static LayerMask m_agentLayerMask { get => Agent.m_agentLayerMask; set => Agent.m_agentLayerMask = value; }
        public static int Mask { get => Agent.Mask; }
        public static Agent s_tempInterface { get => Agent.s_tempInterface; set => Agent.s_tempInterface = value; }
        public AgentType Type { get => this.WrappedObj.Type; set => this.WrappedObj.Type = value; }
        public ushort GlobalID { get => this.WrappedObj.GlobalID; }
        public SNet_Replicator m_replicator { get => this.WrappedObj.m_replicator; set => this.WrappedObj.m_replicator = value; }
        public float DistanceToGoodNode { get => this.WrappedObj.DistanceToGoodNode; set => this.WrappedObj.DistanceToGoodNode = value; }
        public bool m_alwaysAnimatedOnCloseNear { get => this.WrappedObj.m_alwaysAnimatedOnCloseNear; set => this.WrappedObj.m_alwaysAnimatedOnCloseNear = value; }
        public Vector3 TargetLookDir { get => this.WrappedObj.TargetLookDir; set => this.WrappedObj.TargetLookDir = value; }
        public Vector3 Position { get => this.WrappedObj.Position; set => this.WrappedObj.Position = value; }
        public Vector3 m_position { get => this.WrappedObj.m_position; set => this.WrappedObj.m_position = value; }
        public Agent.NoiseType m_noise { get => this.WrappedObj.m_noise; set => this.WrappedObj.m_noise = value; }
        public Vector3 Forward { get => this.WrappedObj.Forward; set => this.WrappedObj.Forward = value; }
        public Quaternion Rotation { get => this.WrappedObj.Rotation; set => this.WrappedObj.Rotation = value; }
        public DetectionManager.TargetGroup TargetGroup { get => this.WrappedObj.TargetGroup; set => this.WrappedObj.TargetGroup = value; }
        public bool InTargetGroup { get => this.WrappedObj.InTargetGroup; set => this.WrappedObj.InTargetGroup = value; }
        public AgentType m_type { get => this.WrappedObj.m_type; set => this.WrappedObj.m_type = value; }
        public bool m_isBeingDestroyed { get => this.WrappedObj.m_isBeingDestroyed; set => this.WrappedObj.m_isBeingDestroyed = value; }
        public Agent.NoiseType Noise { get => this.WrappedObj.Noise; set => this.WrappedObj.Noise = value; }
        public Transform AimTarget { get => this.WrappedObj.AimTarget; }
        public Transform TentacleTarget { get => this.WrappedObj.TentacleTarget; }
        public bool Alive { get => this.WrappedObj.Alive; set => this.WrappedObj.Alive = value; }
        public Vector3 EyePosition { get => this.WrappedObj.EyePosition; }
        public Vector3 NavigationPosition { get => this.WrappedObj.NavigationPosition; set => this.WrappedObj.NavigationPosition = value; }
        public AIG_CourseNode CourseNode { get => this.WrappedObj.CourseNode; set => this.WrappedObj.CourseNode = value; }
        public AIG_INode GoodNode { get => this.WrappedObj.GoodNode; set => this.WrappedObj.GoodNode = value; }
        public Vector3 GoodPosition { get => this.WrappedObj.GoodPosition; set => this.WrappedObj.GoodPosition = value; }
        public AIG_NodeCluster GoodNodeCluster { get => this.WrappedObj.GoodNodeCluster; set => this.WrappedObj.GoodNodeCluster = value; }
        public bool IsLocallyOwned { get => this.WrappedObj.IsLocallyOwned; set => this.WrappedObj.IsLocallyOwned = value; }

        public static bool GetInterface(GameObject game_object, ref Agent agent_interface) => Agent.GetInterface(game_object, ref agent_interface);
        public bool DisableAnimatorCullingWhenRenderingShadow() => this.WrappedObj.DisableAnimatorCullingWhenRenderingShadow();
        public AIG_CourseNode GetCourseNode() => this.WrappedObj.GetCourseNode();
        public int GetPriorityID() => this.WrappedObj.GetPriorityID();
        public void RegisterDamageInflictor(Agent inflictor) => this.WrappedObj.RegisterDamageInflictor(inflictor);
        public void SetAnimatorCullingEnabled(bool mode) => this.WrappedObj.SetAnimatorCullingEnabled(mode);
        public void SetCourseNode(AIG_CourseNode courseNode) => this.WrappedObj.SetCourseNode(courseNode);
        public void Start() => this.WrappedObj.Start();
    }
}
