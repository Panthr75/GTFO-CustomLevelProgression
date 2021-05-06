using CustomLevelProgression.Wrappers.WSystem;
using UnhollowerBaseLib;

namespace CustomLevelProgression.Wrappers.WUnhollowerBaseLib
{
    public class Il2CppObjectBaseWrapper : ObjectWrapper
    {
        public new Il2CppObjectBase WrappedObj => (Il2CppObjectBase)base.obj;

        public Il2CppObjectBaseWrapper(Il2CppObjectBase obj) : base(obj)
        { }

        public System.IntPtr Pointer { get => WrappedObj.Pointer; }

        public T Cast<T>() where T : Il2CppObjectBase => WrappedObj.Cast<T>();
        public T TryCast<T>() where T : Il2CppObjectBase => WrappedObj.TryCast<T>();
        public T Unbox<T>() where T : unmanaged => WrappedObj.Unbox<T>();
    }
}