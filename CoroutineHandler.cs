using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLevelProgression
{
    internal class CoroutineHandler : IL2CPP.Il2CppSingleton
    {
        private List<Routine> routines;
        private static CoroutineHandler instance;

        public CoroutineHandler(System.IntPtr value) : base(value) { }

        internal static void Create()
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("Coroutine Handler");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<CoroutineHandler>();

                IL2CPP.Il2CppTypeRegistry.AddBehaviour(instance);
            }
        }

        public static void Add(IEnumerator enumerator, bool stopWhenExitLevel = false)
        {
            if (enumerator != null)
                instance.routines.Add(new Routine(enumerator, stopWhenExitLevel));
        }

        public void AddRoutine(IEnumerator enumerator, bool stopWhenExitLevel = false)
        {
            if (enumerator != null)
                routines.Add(new Routine(enumerator, stopWhenExitLevel));
        }

        private void Awake()
        {
            instance = this;
            this.routines = new List<Routine>();
        }

        private void Update()
        {
            int index = 0;
            while (index < routines.Count)
            {
                var routine = routines[index];
                if (routine.Tick())
                    this.routines.RemoveAt(index);
                else
                    index++;
            }
        }

        private static IEnumerator WaitForSecondsEnumerator(float seconds)
        {
            float start = Time.time;

            while (Time.time - start < seconds)
                yield return null;
        }

        public override void DestroyInstance()
        {
            this.routines.Clear();
            Destroy(this.gameObject);
            instance = null;
        }

        private class Routine
        {
            private List<IEnumerator> stack;
            private bool stopIfExitLevel;

            public Routine(IEnumerator enumerator, bool stopIfExitLevel)
            {
                this.stopIfExitLevel = stopIfExitLevel;
                this.stack = new List<IEnumerator>();
                this.stack.Add(enumerator);
            }

            public bool Tick()
            {
                if (stack.Count > 0)
                {
                    if (this.stopIfExitLevel && !GameInfo.InLevel)
                    {
                        this.stack.Clear();
                    }
                    else
                    {
                        var current = stack[stack.Count - 1];
                        bool finished = current.MoveNext();
                        if (!finished)
                        {
                            stack.RemoveAt(stack.Count - 1);
                        }
                        else
                        {
                            object result = current.Current;
                            if (result is WaitForSeconds wfs)
                                stack.Add(WaitForSecondsEnumerator(wfs.m_Seconds));
                            else if (result is IEnumerator enumerator)
                                stack.Add(enumerator);
                        }
                    }
                }

                return stack.Count == 0;
            }
        }
    }
}