using CustomLevelProgression.Wrappers.WUnhollowerBaseLib;
using Il2CppSystem;

namespace CustomLevelProgression.Wrappers.WIl2CppSystem
{
    public class ObjectWrapper : Il2CppObjectBaseWrapper
    {
        public new Object WrappedObj => (Object)base.obj;

        public ObjectWrapper(Object obj) : base(obj) { }

        public static bool Equals(Object objA, Object objB) => Object.Equals(objA, objB);
        public static int InternalGetHashCode(Object o) => Object.InternalGetHashCode(o);
        public bool Equals(Object obj) => this.WrappedObj.Equals(obj);
        public void FieldGetter(string typeName, string fieldName, ref Object val) => this.WrappedObj.FieldGetter(typeName, fieldName, ref val);
        public void FieldSetter(string typeName, string fieldName, Object val) => this.WrappedObj.FieldSetter(typeName, fieldName, val);
        public void Il2CppFinalize() => this.WrappedObj.Finalize();
        public int GetIl2CppHashCode() => this.WrappedObj.GetHashCode();
        public Type GetIl2CppType() => this.WrappedObj.GetIl2CppType();
        public Object Il2CppMemberwiseClone() => this.WrappedObj.MemberwiseClone();
        public string Il2CppToString() => this.WrappedObj.ToString();
    }
}
