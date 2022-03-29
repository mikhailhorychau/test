namespace UIScripts.Arch
{
    public abstract class Storage
    {
        public virtual void OnCreate() {}
        public virtual void OnStart() {}
        public abstract void Initialize();
    }
}