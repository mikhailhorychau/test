using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils
{
    [Serializable]
    public class UniquePairContainer<TKey, TValue>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        public void Add(TKey key, TValue value)
        {
            if (keys.Contains(key) || values.Contains(value)) return;
            
            keys.Add(key);
            values.Add(value);
        }

        public TValue Get(TKey key)
        {
            if (keys.Contains(key))
            {
                return values[keys.IndexOf(key)];
            }

            return default;
        }

        public TKey Get(TValue value)
        {
            if (values.Contains(value))
            {
                return keys[values.IndexOf(value)];
            }

            return default;
        }
    }
}