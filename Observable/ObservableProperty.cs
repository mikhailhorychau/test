using System;
using UnityEngine;

namespace UIScripts.Observable
{
    [Serializable]
    public class ObservableProperty<T> 
    {
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;

            set
            {
                _value = value;
                OnValueChange?.Invoke(value);   
            }
        }

        public void SetValue(T value) => Value = value;

        public ObservableProperty()
        {
            _value = default;
        }
        
        public ObservableProperty(T value)
        {
            _value = value;
        }
        
        public event ValueChange<T> OnValueChange;

        public static implicit operator T(ObservableProperty<T> source)
        {
            return source.Value;
        }
    }
    
    public delegate void ValueChange<in T>(T value);
}