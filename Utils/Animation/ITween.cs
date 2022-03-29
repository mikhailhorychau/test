using System;

namespace UIScripts.Utils.Animation
{
    public interface ITween
    {
        public event Action OnComplete; 
        public bool IsValidTarget { get; }
        public void SetValue(float value);
        public bool Completed { get; set; }
        public bool Paused { get; set; }
        public float Duration { get; }
        
        public float ElapsedTime { get; set; }
    }
}