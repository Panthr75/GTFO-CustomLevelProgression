using UnityEngine;

namespace CustomLevelProgression.Wrappers.WUnityEngine
{
    public class BehaviourWrapper : ComponentWrapper
    {
        public new Behaviour WrappedObj => (Behaviour)base.obj;

        public BehaviourWrapper(Behaviour obj) : base(obj)
        { }

        public bool enabled { get => this.WrappedObj.enabled; set => this.WrappedObj.enabled = value; }
        public bool isActiveAndEnabled { get => this.WrappedObj.isActiveAndEnabled; }
    }
}
