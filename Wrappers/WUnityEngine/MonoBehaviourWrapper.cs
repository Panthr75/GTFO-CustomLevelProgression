using UnityEngine;

namespace CustomLevelProgression.Wrappers.WUnityEngine
{
    public class MonoBehaviourWrapper : BehaviourWrapper
    {
        public new MonoBehaviour WrappedObj => (MonoBehaviour)base.obj;

        public MonoBehaviourWrapper(MonoBehaviour obj) : base(obj)
        { }

        public bool useGUILayout { get => this.WrappedObj.useGUILayout; set => this.WrappedObj.useGUILayout = value; }

        public static void CancelInvoke(MonoBehaviour self, string methodName) => MonoBehaviour.CancelInvoke(self, methodName);
        public static void Internal_CancelInvokeAll(MonoBehaviour self) => MonoBehaviour.Internal_CancelInvokeAll(self);
        public static bool Internal_IsInvokingAll(MonoBehaviour self) => MonoBehaviour.Internal_IsInvokingAll(self);
        public static void InvokeDelayed(MonoBehaviour self, string methodName, float time, float repeatRate) => MonoBehaviour.InvokeDelayed(self, methodName, time, repeatRate);
        public static bool IsInvoking(MonoBehaviour self, string methodName) => MonoBehaviour.IsInvoking(self, methodName);
        public static bool IsObjectMonoBehaviour(Object obj) => MonoBehaviour.IsObjectMonoBehaviour(obj);
        public static void print(Il2CppSystem.Object message) => MonoBehaviour.print(message);
        public void CancelInvoke() => this.WrappedObj.CancelInvoke();
        public void CancelInvoke(string methodName) => this.WrappedObj.CancelInvoke(methodName);
        public string GetScriptClassName() => this.WrappedObj.GetScriptClassName();
        public void Invoke(string methodName, float time) => this.WrappedObj.Invoke(methodName, time);
        public void InvokeRepeating(string methodName, float time, float repeatRate) => this.WrappedObj.InvokeRepeating(methodName, time, repeatRate);
        public bool IsInvoking(string methodName) => this.WrappedObj.IsInvoking(methodName);
        public bool IsInvoking() => this.WrappedObj.IsInvoking();
        public Coroutine StartCoroutine(string methodName, Il2CppSystem.Object value) => this.WrappedObj.StartCoroutine(methodName, value);
        public Coroutine StartCoroutine(Il2CppSystem.Collections.IEnumerator routine) => this.WrappedObj.StartCoroutine(routine);
        public Coroutine StartCoroutine(string methodName) => this.WrappedObj.StartCoroutine(methodName);
        public Coroutine StartCoroutineManaged(string methodName, Il2CppSystem.Object value) => this.WrappedObj.StartCoroutineManaged(methodName, value);
        public Coroutine StartCoroutineManaged2(Il2CppSystem.Collections.IEnumerator enumerator) => this.WrappedObj.StartCoroutineManaged2(enumerator);
        public Coroutine StartCoroutine_Auto(Il2CppSystem.Collections.IEnumerator routine) => this.WrappedObj.StartCoroutine_Auto(routine);
        public void StopAllCoroutines() => this.WrappedObj.StopAllCoroutines();
        public void StopCoroutine(Il2CppSystem.Collections.IEnumerator routine) => this.WrappedObj.StopCoroutine(routine);
        public void StopCoroutine(string methodName) => this.WrappedObj.StopCoroutine(methodName);
        public void StopCoroutine(Coroutine routine) => this.WrappedObj.StopCoroutine(routine);
        public void StopCoroutineFromEnumeratorManaged(Il2CppSystem.Collections.IEnumerator routine) => this.WrappedObj.StopCoroutineFromEnumeratorManaged(routine);
        public void StopCoroutineManaged(Coroutine routine) => this.WrappedObj.StopCoroutineManaged(routine);
    }
}