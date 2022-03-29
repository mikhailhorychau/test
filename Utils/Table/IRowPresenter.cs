namespace UIScripts.Table
{
    public interface IRowPresenter<in T>
    {
        void Initialize(T data);
    }
}