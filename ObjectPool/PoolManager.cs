using System;
using System.Collections.Generic;

namespace UIScripts.ObjectPool
{
    public class PoolManager
    {
        private Dictionary<Type, object> _pools = new Dictionary<Type, object>(); 
        
        public Pool<T> MakePool<T>(Func<T> createObjFunc, Action<T> returnObjAction, int count)
        {
            var pool = new Pool<T>(createObjFunc, returnObjAction, count);
            var type = typeof(T);

            if (_pools.ContainsKey(type))
                return (Pool<T>) _pools[type];
            
            _pools.Add(type, pool);

            return pool;
        }

        public T GetObject<T>()
        {
            var type = typeof(T);
            
            if (_pools.ContainsKey(type))
            {
                var pool = (Pool<T>) _pools[type];

                return pool.GetObject();
            }

            return default;
        }

        public void ReturnObject<T>(T obj)
        {
            var type = typeof(T);
            
            if (_pools.ContainsKey(type))
            {
                var pool = (Pool<T>) _pools[type];

                pool.ReturnObject(obj);
            }
        }
    }
}