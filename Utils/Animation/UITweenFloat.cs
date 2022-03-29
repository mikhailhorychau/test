using System;
using UnityEngine;

namespace UIScripts.Utils.Animation
{
    public struct UITweenFloat : ITween
    {
        private GameObject _target;
        private float _from;
        private float _to;
        private float _value;
        private bool _completed;

        public event Action<float> OnValueChanged;
        public event Action OnComplete;
        public bool IsValidTarget => _target != null;

        public bool Completed
        {
            get => _completed;
            set
            {
                if (_completed.Equals(value))
                    return;
                
                _completed = value;
                if (_completed)
                    OnComplete?.Invoke();
            }
        }
        
        public bool Paused { get; set; }
        public float Duration { get; }
        
        public float ElapsedTime { get; set; }

        public float Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public UITweenFloat(GameObject target, float from, float to, float duration) : this()
        {
            _target = target;
            _from = from;
            _to = to;

            Duration = duration;
            Completed = false;
            Paused = false;
            Duration = duration;
            ElapsedTime = 0f;
        }
        
        public UITweenFloat(GameObject target, float duration) : this(target, 0, 0, duration) {}

        public void SetValue(float value)
        {
            Value = Mathf.Lerp(_from, _to, value);
        }
        
    }
}