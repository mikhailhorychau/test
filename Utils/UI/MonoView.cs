using System;
using UnityEngine;

namespace UIScripts.Utils.UI
{
    public abstract class MonoView : MonoBehaviour
    {
        public event Action OnInitialized;

        private void Awake()
        {
            OnAwake();
            OnInitialized?.Invoke();
        }

        protected abstract void OnAwake();
    }
}