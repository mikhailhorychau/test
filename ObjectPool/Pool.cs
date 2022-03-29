using System;
using System.Collections.Generic;
using System.Linq;

namespace UIScripts.ObjectPool
{
    public class Pool<T> 
    {
        private readonly Dictionary<T, PoolItem<T>> _objects;
        private readonly Func<T> _createObjFunc;
        private readonly Action<T> _returnObjAction;

        public Pool(Func<T> createObjFunc, Action<T> returnObjAction, int size)
        {
            _createObjFunc = createObjFunc;
            _returnObjAction = returnObjAction;
            _objects = new Dictionary<T, PoolItem<T>>();
            for (var index = 0; index < size; index++)
            {
                CreateNewObject(true);
            }
        }

        public T GetObject()
        {
            var item = _objects.FirstOrDefault(pair => pair.Value.Free).Value;
            if (item != null)
            {
                item.Free = false;
                return item.ItemObject;
            }

            return CreateNewObject(false);
        }

        public void ReturnObject(T item)
        {
            if (_objects.ContainsKey(item))
            {
                _objects[item].Free = true;
                _returnObjAction(item);
            }
        }

        private T CreateNewObject(bool isFree)
        {
            var obj = _createObjFunc();
            var itemObj = new PoolItem<T>(obj, isFree);
            _objects.Add(obj, itemObj);
            return obj;
        }
    }
}