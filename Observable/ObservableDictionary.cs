using System;
using System.Collections.Generic;

namespace UIScripts.Observable
{
    public class ObservableDictionary<TKey, TValue>
    {
        public event Action<Dictionary<TKey, TValue>> OnReinitialize;
        public event Action<TKey, TValue> OnAdded;
        public event Action<TKey, TValue> OnUpdated;
        public event Action<TKey> OnRemoved;

        public Dictionary<TKey, TValue> Dictionary { get; protected set; } = new Dictionary<TKey, TValue>();

        public void SetDictionary(Dictionary<TKey, TValue> newDictionary)
        {
            Dictionary = newDictionary;
            OnReinitialize?.Invoke(Dictionary);
        }

        public void Add(TKey key, TValue value)
        {
            if (Dictionary.ContainsKey(key)) return;
            
            Dictionary.Add(key, value);
            
            OnAdded?.Invoke(key, value);
        }

        public void Update(TKey key, TValue value)
        {
            if (!Dictionary.ContainsKey(key)) return;

            Dictionary[key] = value;
            
            OnUpdated?.Invoke(key, value);
        }

        public void Remove(TKey key)
        {
            if (!Dictionary.ContainsKey(key)) return;

            Dictionary.Remove(key);
            
            OnRemoved?.Invoke(key);
        }
    }
}