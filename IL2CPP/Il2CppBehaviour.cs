using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
