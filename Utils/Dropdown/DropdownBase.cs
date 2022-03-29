using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public abstract class DropdownBase<TProps, TPresenter> : 
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler,
        IPointerUpHandler,
        IDeselectHandler,
        IDropdown<TProps> 
        where TPresenter : IDropdownItemPresenter<TProps> 
        where TProps : IProps
    {
        [SerializeField] protected Transform content;
        [SerializeField] protected GameObject container;
        [SerializeField] protected GameObject itemPrefab;

        [SerializeField] protected TextMeshProUGUI placeholder;
        [SerializeField] protected string placeholderText;
        [SerializeField] protected bool isDisabled;

        public bool IsDisabled
        {
            get => isDisabled;
            set => isDisabled = value;
        }

        public string PlaceholderText
        {
            get => placeholderText;
            set
            {
                placeholderText = value;
                placeholder.text = value;
            }
        }

        [SerializeField] protected GameObject placeholderPresenter;
        
        [SerializeField] protected List<TProps> items = new List<TProps>();

        public UnityEvent<int> onValueChange;

        public UnityEvent<int> OnValueChange => onValueChange;
        public UnityEvent<bool> OnDropdownStateChange;
        
        public bool isFirstSelected;
        
        protected int currentValue = -1;

        public int CurrentValue
        {
            get => currentValue;
            set => UpdateCurrentValue(value);
        }

        protected bool isOpen;
        protected bool mouseIsOver;

        private void Awake()
        {
            // InitItems();
        }

        public void Initialize(List<TProps> props)
        {
            // items.Clear();
            items = props;
            currentValue = -1;
            InitItems();
        }
        protected void InitItems()
        {
            if (placeholder) 
                placeholder.text = placeholderText;
            ClearContent();
            items.ForEach(InitItem);
            if (isFirstSelected) SetFirst();
        }

        protected virtual void InitItem(TProps props)
        {
            var item = Instantiate(itemPrefab, content);
            item.GetComponent<TPresenter>().Initialize(props);
            
            var controller = item.GetComponent<ObjectPointerController>();
            controller.onPointerClick.AddListener(() =>
            {
                isOpen = false;
                container.SetActive(false);
                if (props.GetId() == currentValue) return;
                UpdateCurrentValue(props.GetId());
                UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            });
            
            var contentWidth = content.GetComponent<RectTransform>().rect.width;
            var itemRect = item.GetComponent<RectTransform>();
            itemRect.sizeDelta = new Vector2(contentWidth, itemRect.sizeDelta.y);
        }

        protected virtual void UpdateCurrentValue(int value)
        {
            currentValue = value;
            isOpen = false;
            
            container.SetActive(false);
            onValueChange.Invoke(value);
            UpdatePlaceholder(items.FindLast(item => item.GetId() == value));
        }

        public virtual void UpdateCurrentValueWithoutNotification(int value)
        {
            currentValue = value;
            isOpen = false;
            
            container.SetActive(false);
            var item = items.FindLast(item => item.GetId() == value);
            UpdatePlaceholder(item);
        }

        public virtual void SetFirst()
        {
            if (items.Count > 0)
                UpdateCurrentValue(items[0].GetId());
        }

        public virtual void SetLast()
        {
            if (items.Count > 0)
                UpdateCurrentValue(items.Last().GetId());
        }

        public virtual void SetNext()
        {
            if (currentValue == items.Last().GetId()) return;
            
            UpdateCurrentValue(currentValue + 1);
        }

        public virtual void SetPrev()
        {
            if (currentValue <= 1) return;
            
            UpdateCurrentValue(currentValue - 1);
        }

        protected abstract void UpdatePlaceholder(TProps value);

        private void ClearContent()
        {
            foreach (Transform obj in content)
            {
                Destroy(obj.gameObject);
            }
        }
        

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            mouseIsOver = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            mouseIsOver = false;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (isDisabled) return;
            if (eventData.pointerPress.GetComponent<Scrollbar>()) return;
            isOpen = !isOpen;
            OnDropdownStateChange.Invoke(isOpen);
            
            container.SetActive(isOpen);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
            if (!mouseIsOver) Deselect();
        }

        public virtual void Deselect()
        {
            isOpen = false;
            OnDropdownStateChange.Invoke(isOpen);
            container.SetActive(false);
        }

        public void SetInteractivity(bool interactive)
        {
            IsDisabled = !interactive;
        }
    }
}