using System;
using System.Collections.Generic;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.DropdownButton
{
    public class PureDropdown<TItem, TData> : MonoBehaviour, IPointerClickHandler, ICancelHandler, ISelectHandler, ISubmitHandler
        where TItem : PureDropdownItem<TData>
        where TData : IDropdownItemData
    {
        public event Action<int> OnItemChoose;

        [SerializeField] private RectTransform container;
        [SerializeField] protected TItem prefab;

        protected Canvas _containerCanvas;
        private Blocker _blocker;

        private Dictionary<int, TItem> _items = new Dictionary<int, TItem>();

        private bool _isOpen = false;

        public IEnumerable<TItem> Items => _items.Values;

        protected void Awake()
        {
            _containerCanvas = container.GetComponent<Canvas>();
            _containerCanvas.sortingOrder = 1000;
            _containerCanvas.enabled = false;
            
            OnAwake();
        }

        public void Initialize(List<TData> data)
        {
            container.Clear();
            _items.Clear();
            data.ForEach(itemData =>
            {
                var item = CreateItem(itemData);
                item.Initialize(itemData);
                AddItem(item);
            });
        }

        public virtual TItem CreateItem(TData data) => Instantiate(prefab);

        public void AddItem(TItem item)
        {
            item.transform.SetParent(container);
            item.OnChoose += ItemChooseListener;
            item.OnCancelEvt += Hide;
            
            _items.Add(item.Data.ID, item);
        }

        public void RemoveItem(int id)
        {
            if (_items.TryGetValue(id, out var item))
            {
                _items.Remove(id);
                Destroy(item);
            }
        }

        public void Hide()
        {
            _isOpen = false;
            _containerCanvas.enabled = false;
            OnHide();
            
            if (_blocker != null)
            {
                Destroy(_blocker.gameObject);
                _blocker = null;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Toggle();
        }
        
        public void OnCancel(BaseEventData eventData)
        {
            Hide();
        }

        public void OnSelect(BaseEventData eventData)
        {
            
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Toggle();
        }

        private void Toggle()
        {
            if (_isOpen)
                Hide();
            else
            {
                Show();
            }
        }
        
        private void ItemChooseListener(int id)
        {
            Hide();
            OnItemChoose?.Invoke(id);
        }

        private Blocker CreateBlocker()
        {
            var root = gameObject.GetRootCanvas();

            var blocker = Blocker.Create(root, _containerCanvas.sortingLayerID, _containerCanvas.sortingOrder);
            blocker.OnClick += Hide;

            return blocker;
        }
        
        
        public void Show()
        {
            _isOpen = true;
            _containerCanvas.enabled = true;
            _blocker = CreateBlocker();
            OnShow();
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        protected virtual void OnShow()
        {

        }
        protected virtual void OnHide() {}
        
        protected virtual void OnAwake() {}
    }
}