using UnhollowerBaseLib;
using UnityEngine;

namespace CustomLevelProgression.Wrappers.WUnityEngine
{
    public class ComponentWrapper : ObjectWrapper
    {
        public new Component WrappedObj => (Component)base.obj;

        public ComponentWrapper(Component obj) : base(obj)
        { }

        public string tag { get => this.WrappedObj.tag; }
        public Transform transform { get => this.WrappedObj.transform; }
        public GameObject gameObject { get => this.WrappedObj.gameObject; }

        public Component GetComponent(Il2CppSystem.Type type) => this.WrappedObj.GetComponent(type);
        public T GetComponent<T>() => this.WrappedObj.GetComponent<T>();
        public void GetComponentFastPath(Il2CppSystem.Type type, System.IntPtr oneFurtherThanResultValue) => this.WrappedObj.GetComponentFastPath(type, oneFurtherThanResultValue);
        public T GetComponentInChildren<T>() => this.WrappedObj.GetComponentInChildren<T>();
        public Component GetComponentInChildren(Il2CppSystem.Type t, bool includeInactive) => this.WrappedObj.GetComponentInChildren(t, includeInactive);
        public T GetComponentInChildren<T>(bool includeInactive) => this.WrappedObj.GetComponentInChildren<T>(includeInactive);
        public Component GetComponentInParent(Il2CppSystem.Type t) => this.WrappedObj.GetComponentInParent(t);
        public T GetComponentInParent<T>() => this.WrappedObj.GetComponentInParent<T>();
        public Il2CppArrayBase<T> GetComponents<T>() => this.WrappedObj.GetComponents<T>();
        public void GetComponents(Il2CppSystem.Type type, Il2CppSystem.Collections.Generic.List<Component> results) => this.WrappedObj.GetComponents(type, results);
        public void GetComponents<T>(Il2CppSystem.Collections.Generic.List<T> results) => this.WrappedObj.GetComponents<T>(results);
        public void GetComponentsForListInternal(Il2CppSystem.Type searchType, Il2CppSystem.Object resultList) => this.WrappedObj.GetComponentsForListInternal(searchType, resultList);
        public void GetComponentsInChildren<T>(bool includeInactive, Il2CppSystem.Collections.Generic.List<T> result) => this.WrappedObj.GetComponentsInChildren<T>(includeInactive, result);
        public void GetComponentsInChildren<T>(Il2CppSystem.Collections.Generic.List<T> results) => this.WrappedObj.GetComponentsInChildren<T>(results);
        public Il2CppArrayBase<T> GetComponentsInChildren<T>() => this.WrappedObj.GetComponentsInChildren<T>();
        public Il2CppArrayBase<T> GetComponentsInChildren<T>(bool includeInactive) => this.WrappedObj.GetComponentsInChildren<T>(includeInactive);
        public Il2CppArrayBase<T> GetComponentsInParent<T>() => this.WrappedObj.GetComponentsInParent<T>();
        public void GetComponentsInParent<T>(bool includeInactive, Il2CppSystem.Collections.Generic.List<T> results) => this.WrappedObj.GetComponentsInParent<T>(includeInactive, results);
        public Il2CppArrayBase<T> GetComponentsInParent<T>(bool includeInactive) => this.WrappedObj.GetComponentsInParent<T>(includeInactive);
    }
}
