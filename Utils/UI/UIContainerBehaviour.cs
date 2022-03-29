using System;
using UnityEngine;

namespace UIScripts.Utils.UI
{
    public abstract class UIContainerBehaviour : MonoBehaviour
    {
        public event Action OnContainerContentChanged;
        public event Action OnObjectDestroy;

        [SerializeField] protected RectTransform container;

        public abstract bool IsEmpty();

        protected void RaiseContainerChangedEvent() => OnContainerContentChanged?.Invoke();

        private void OnDestroy()
        {
            OnObjectDestroy?.Invoke();
        }
    }
}