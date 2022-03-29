using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils.Animation
{
    public class UITweenRunner : MonoSingleton<UITweenRunner>
    {
        private Dictionary<int, ITween> _tweens = new Dictionary<int, ITween>();
        private static IdContainer _idContainer = new IdContainer();
        private static Queue<int> _itemsForRemove = new Queue<int>();

        public static int RunFloatTween(GameObject obj, float from, float to, float duration, Action<float> callback)
        {
            var tween = new UITweenFloat(obj, from, to, duration);
            tween.OnValueChanged += callback;

            return RunTween(tween);
        }

        public static int RunTween(ITween tween)
        {
            var id = _idContainer.GetID();
            Instance._tweens[id] = tween;

            return id;
        }

        public static int RunAction(GameObject obj, float duration, Action callback)
        {
            var tween = new UITweenFloat(obj, 0, 0, duration);
            tween.OnComplete += callback;

            return RunTween(tween);
        }

        public static void PauseTween(int id) 
        {
            if (Instance._tweens.TryGetValue(id, out var tween))
            {
                tween.Paused = true;
            }
        }

        public static void ResumeTween(int id)
        {
            if (Instance._tweens.TryGetValue(id, out var tween))
            {
                tween.Paused = false;
            }
        }
        
        public static void FinishTween(int id) 
        {
            if (Instance._tweens.TryGetValue(id, out var tween))
            {
                tween.Completed = true;
                tween.SetValue(1f);
                _itemsForRemove.Enqueue(id);
            }
        }
        public static void CancelTween(int id) 
        {
            if (Instance._tweens.TryGetValue(id, out var tween))
            {
                _itemsForRemove.Enqueue(id);
            }
        }
        
        private void Update()
        {
            if (_tweens.Count == 0) 
                return;
            
            foreach (var keyValuePair in _tweens)
            {
                var id = keyValuePair.Key;
                var tween = keyValuePair.Value;

                if (tween.Paused) 
                    continue;
                
                tween.ElapsedTime += Time.deltaTime;

                if (tween.ElapsedTime >= tween.Duration || tween.Completed)
                {
                    if (_itemsForRemove.Contains(id)) 
                        continue;
                    tween.SetValue(1f);
                    tween.Completed = true;
                    _itemsForRemove.Enqueue(id);
                    continue;
                }

                var percentage = Mathf.Clamp01(tween.ElapsedTime / tween.Duration);
                tween.SetValue(percentage);
            }
            
            while(_itemsForRemove.Count > 0)
            {
                var id = _itemsForRemove.Dequeue();
                _idContainer.Release(id);
                _tweens.Remove(id);
            }
        }
    }
}