using UnhollowerBaseLib;
using UnityEngine;

namespace CustomLevelProgression.Wrappers.WUnityEngine
{
    public class ObjectWrapper : WIl2CppSystem.ObjectWrapper
    {
        public new Object WrappedObj => (Object)base.obj;

        public ObjectWrapper(Object obj) : base(obj)
        { }

        public static string cloneDestroyedMessage { get => Object.cloneDestroyedMessage; set => Object.cloneDestroyedMessage = value; }
        public static string objectIsNullMessage { get => Object.objectIsNullMessage; set => Object.objectIsNullMessage = value; }
        public static int OffsetOfInstanceIDInCPlusPlusObject { get => Object.OffsetOfInstanceIDInCPlusPlusObject; set => OffsetOfInstanceIDInCPlusPlusObject = value; }
        public HideFlags hideFlags { set => this.WrappedObj.hideFlags = value; }
        public System.IntPtr m_CachedPtr { get => this.WrappedObj.m_CachedPtr; set => this.WrappedObj.m_CachedPtr = value; }
        public string name { get => this.WrappedObj.name; set => this.WrappedObj.name = value; }

        public static void CheckNullArgument(Il2CppSystem.Object arg, string message) => Object.CheckNullArgument(arg, message);
        public static bool CompareBaseObjects(Object lhs, Object rhs) => Object.CompareBaseObjects(lhs, rhs);
        public static void Destroy(Object obj, float t) => Object.Destroy(obj, t);
        public static void Destroy(Object obj) => Object.Destroy(obj);
        public static void DestroyImmediate(Object obj, bool allowDestroyingAssets) => Object.DestroyImmediate(obj, allowDestroyingAssets);
        public static void DestroyImmediate(Object obj) => Object.DestroyImmediate(obj);
        public static void DontDestroyOnLoad(Object target) => Object.DontDestroyOnLoad(target);
        public static Object FindObjectFromInstanceID(int instanceID) => Object.FindObjectFromInstanceID(instanceID);
        public static T FindObjectOfType<T>() where T : Object => Object.FindObjectOfType<T>();
        public static Object FindObjectOfType(Il2CppSystem.Type type) => Object.FindObjectOfType(type);
        public static Il2CppReferenceArray<Object> FindObjectsOfType(Il2CppSystem.Type type) => Object.FindObjectsOfType(type);
        public static Il2CppArrayBase<T> FindObjectsOfType<T>() where T : Object => Object.FindObjectsOfType<T>();
        public static string GetName(Object obj) => Object.GetName(obj);
        public static int GetOffsetOfInstanceIDInCPlusPlusObject() => Object.GetOffsetOfInstanceIDInCPlusPlusObject();
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation) => Object.Instantiate(original, position, rotation);
        public static Object Instantiate(Object original) => Object.Instantiate(original);
        public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace) => Object.Instantiate(original, parent, instantiateInWorldSpace);
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object => Object.Instantiate<T>(original, position, rotation, parent);
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object => Object.Instantiate<T>(original, position, rotation);
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent) => Object.Instantiate(original, position, rotation, parent);
        public static T Instantiate<T>(T original, Transform parent) where T : Object => Object.Instantiate<T>(original, parent);
        public static T Instantiate<T>(T original) where T : Object => Object.Instantiate<T>(original);
        public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object => Object.Instantiate<T>(original, parent, worldPositionStays);
        public static Object Internal_CloneSingle(Object data) => Object.Internal_CloneSingle(data);
        public static Object Internal_CloneSingleWithParent(Object data, Transform parent, bool worldPositionStays) => Object.Internal_CloneSingleWithParent(data, parent, worldPositionStays);
        public static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot) => Object.Internal_InstantiateSingle(data, pos, rot);
        public static Object Internal_InstantiateSingleWithParent(Object data, Transform parent, Vector3 pos, Quaternion rot) => Object.Internal_InstantiateSingleWithParent(data, parent, pos, rot);
        public static Object Internal_InstantiateSingleWithParent_Injected(Object data, Transform parent, ref Vector3 pos, ref Quaternion rot) => Object.Internal_InstantiateSingleWithParent_Injected(data, parent, ref pos, ref rot);
        public static Object Internal_InstantiateSingle_Injected(Object data, ref Vector3 pos, ref Quaternion rot) => Object.Internal_InstantiateSingle_Injected(data, ref pos, ref rot);
        public static bool IsNativeObjectAlive(Object o) => Object.IsNativeObjectAlive(o);
        public static void SetName(Object obj, string name) => Object.SetName(obj, name);
        public static string ToString(Object obj) => Object.ToString(obj);
        public System.IntPtr GetCachedPtr() => this.WrappedObj.GetCachedPtr();
        public int GetInstanceID() => this.WrappedObj.GetInstanceID();
    }
}
