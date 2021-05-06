namespace CustomLevelProgression.IL2CPP
{
    public class Il2CppSingleton : Il2CppBehaviour
    {
        public Il2CppSingleton(System.IntPtr value) : base(value)
        { }

        public virtual void DestroyInstance() => throw new System.NotImplementedException();
    }
}