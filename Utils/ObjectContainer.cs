using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.Utils
{
    public abstract class ObjectContainer<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] private List<T> empty;

        private Dictionary<int, T> _objects = new Dictionary<int, T>();

        protected IEnumerable<T> ContentObjects => _objects.Values;
        
        public T Initialize(int id)
        {
            var obj = Get();
            _objects[id] = obj;
            return obj;
        }

        public void Remove(int id)
        {
            if (_objects.TryGetValue(id, out var obj))
            {
                empty.Add(obj);
                obj.gameObject.SetActive(false);
                _objects.Remove(id);
            }
        }

        private T Get()
        {
            if (empty.Count > 0)
            {
                var index = empty.Count - 1;
                var obj = empty[index];
                empty.RemoveAt(index);
                obj.gameObject.SetActive(true);
                return obj;
            }

            return Instantiate(prefab);
        }
        
        public virtual void OnContainerContentChanged() {}
    }
}