namespace UIScripts.ObjectPool
{
    public class PoolItem<T>
    {
        public T ItemObject;
        public bool Free = true;

        public PoolItem(T item, bool free)
        {
            ItemObject = item;
            Free = free;
        }
    }
}