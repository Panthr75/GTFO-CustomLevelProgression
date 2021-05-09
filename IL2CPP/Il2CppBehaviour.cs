using UnityEngine;

namespace CustomLevelProgression.IL2CPP
{
    public class Il2CppBehaviour : MonoBehaviour
    {
        public Il2CppBehaviour(System.IntPtr value) : base(value)
        { }

        public virtual void OnRemovedFromRegistry()
        {

        }
    }
}