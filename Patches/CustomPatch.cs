using System;
using System.Reflection;
using HarmonyLib;

namespace CustomLevelProgression.Patches
{
    public abstract class CustomPatch : PatchBase
    {
        private Harmony harmony;
        private bool patched;

        private PatchType patchType;
        private Type type;
        private MethodInfo method;

        private HarmonyMethod harmonyMethod;

        public override bool Patched => this.patched;
        public Type PatchedType => type;

        public CustomPatch(Harmony harmony, PatchType patchType, Type type, string methodName)
        {
            this.patched = false;

            this.harmony = harmony;
            this.patchType = patchType;
            this.type = type;

            this.method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            System.Console.WriteLine($"Method is null: {method == null}");

            this.harmonyMethod = new HarmonyMethod(this.GetType().GetMethod("Invoke", BindingFlags.Public | BindingFlags.Static));
        }

        public CustomPatch(Harmony harmony, PatchType patchType, Type type, string methodName, Type[] parameters)
        {
            this.patched = false;

            this.harmony = harmony;
            this.patchType = patchType;
            this.type = type;

            if (parameters != null)
            {
                this.method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance, null, parameters, null);
            }
            else
            {
                this.method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            }

            this.harmonyMethod = new HarmonyMethod(this.GetType().GetMethod("Invoke", BindingFlags.Public | BindingFlags.Static));
        }

        public override void Patch()
        {
            if (!this.patched)
            {
                switch (this.patchType)
                {
                    case PatchType.Prefix:
                        this.harmony.Patch(this.method, prefix: this.harmonyMethod);
                        this.patched = true;
                        break;
                    case PatchType.Postfix:
                        this.harmony.Patch(this.method, postfix: this.harmonyMethod);
                        this.patched = true;
                        break;
                    case PatchType.Finalizer:
                        this.harmony.Patch(this.method, finalizer: this.harmonyMethod);
                        this.patched = true;
                        break;
                    case PatchType.Transpiler:
                        this.harmony.Patch(this.method, transpiler: this.harmonyMethod);
                        this.patched = true;
                        break;
                }
            }
        }

        public override void UnPatch()
        {
            if (this.patched)
            {
                this.harmony.Unpatch(this.method, this.harmonyMethod.method);
                this.patched = false;
            }
        }
    }
}
