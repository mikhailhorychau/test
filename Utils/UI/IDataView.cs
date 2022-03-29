namespace UIScripts.Utils.UI
{
    public interface IDataView<TData> where TData : IUIData
    {
        public TData GetCurrentData();
        
        public void Initialize(TData data);
    }
}