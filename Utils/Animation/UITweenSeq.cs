using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.Utils.Animation
{
    public struct UITweenSeq : ITween
    {
        private GameObject _target;
        private bool _completed;
        private Queue<ITween> _tweens;
        private ITween _current;
        private float _prevDuration;

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
                {
                    if (!_current.Completed)
                    {
                        _current.SetValue(1f);
                        _current.Completed = true;
                    }
                    
                    while (_tweens.Count > 0)
                    {
                        var tween = _tweens.Dequeue();
                        tween.SetValue(1f);
                        tween.Completed = true;
                    }
                    
                    OnComplete?.Invoke();
                }
            }
        }

        public bool Paused { get; set; }

        public float Duration { get; private set; }

        public float ElapsedTime { get; set; }

        private float TotalTime { get; set; }

        public UITweenSeq(GameObject target) : this()
        {
            _target = target;
            _tweens = new Queue<ITween>();
        }

        public void SetValue(float value)
        {
            if (Completed)
                return;

            if (_tweens.Count == 0 && Completed)
            {
                return;
            }
            
            if (_current == null)
            {
                _current = _tweens.Dequeue();
            }
            
            var seqTime = Duration * value;
            
            if (seqTime > _current.Duration + _prevDuration)
            {
                _prevDuration += _current.Duration;
                
                if (_tweens.Count == 0)
                {
                    Completed = true;
                    return;
                }

                _current.Completed = true;
                _current = _tweens.Dequeue();
            }

            var tweenTime = seqTime - _prevDuration;
            var tweenProgress = Mathf.Clamp01(tweenTime / _current.Duration);
            _current.SetValue(tweenProgress);
        }

        public void Append(ITween tween)
        {
            _tweens.Enqueue(tween);
            Duration += tween.Duration;
        }

        public void Append(Action action)
        {
            var tween = new UITweenFloat(_target, 0);
            tween.OnComplete += action;
            Append(tween);
        }
    }
}