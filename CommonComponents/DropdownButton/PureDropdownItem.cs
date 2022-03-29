using System;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts.CommonComponents.DropdownButton
{
    public abstract class PureDropdownItem<T> : MonoBehaviour, IDropdownItem<T>, IPointerEnterHandler, IPointerExitHandler, ICancelHandler 
        where T : IDropdownItemData
    {
        public event Action<int> OnChoose;
        public event Action OnCancelEvt;
        public event Action<bool> OnInteractableChanged;
        public event Action OnEnableEvt;

        [SerializeField] private Selectable selectable;

        private bool _interactable = true;
        public T Data { get; protected set; }

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                selectable.interactable = value;
                OnInteractableChanged?.Invoke(value);
            }
        }

        private void OnEnable()
        {
            OnEnableEvt?.Invoke();
        }

        public virtual void Initialize(T data)
        {
            Data = data;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            SceneNavigation.EventSystemCurrent.SetSelectedGameObject(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }

        public void OnCancel(BaseEventData eventData)
        {
            if (!_interactable)
                return;
            
            OnCancelEvt?.Invoke();
        }

        protected void RaiseEvent()
        {
            OnChoose?.Invoke(Data.ID);
        }
    }

    public interface IDropdownItem<T> where T : IDropdownItemData
    {
        public T Data { get; }

        public void Initialize(T data);
    }

    public interface IDropdownItemData
    {
        int ID { get; }
    }
}