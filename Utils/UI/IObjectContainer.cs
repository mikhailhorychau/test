namespace UIScripts.Utils.UI
{
    public interface IObjectContainer<in T> where T : IUIData
    {
        public void InitializeItem(T data);
        public bool UpdateItem(int id, T data);
        public bool RemoveItem(int id);
    }
}