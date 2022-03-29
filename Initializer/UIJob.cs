using System;
using System.Collections;
using UnityEngine;

namespace UIScripts.Initializer
{
    public class UIJob
    {
        public event Action OnComplete;
        private Action _action;
        public bool IsComplete { get; private set; }

        
        public UIJob(Action action)
        {
            _action = action;
            IsComplete = false;
        }

        public virtual IEnumerator Execute()
        {
            _action.Invoke();
            yield return null;
            IsComplete = true;
            OnComplete?.Invoke();
        }
    }
}