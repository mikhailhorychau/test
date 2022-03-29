using UnityEngine;

namespace UIScripts
{
    public class PlusMinusDropdown : StyledStringDropdown
    {
        [SerializeField] public int maxValue;

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                InitElements();
                InitItems();
            }
        }

        private void Awake()
        {
            InitElements();
            UpdateCurrentValue(5);
        }

        protected override void InitItem(StringProps props)
        {
            var item = Instantiate(itemPrefab, content);
            item.GetComponent<StyledStringDropdownItem>().Initialize(props);
            
            var controller = item.GetComponent<ObjectPointerController>();
            controller.onPointerClick.AddListener(() => UpdateCurrentValue(props.GetId()));
            
            var contentWidth = content.GetComponent<RectTransform>().rect.width;
            var itemRect = item.GetComponent<RectTransform>();
            itemRect.sizeDelta = new Vector2(contentWidth, itemRect.sizeDelta.y);
        }
        

        protected override void UpdateCurrentValue(int value)
        {
            currentValue = value;
            isOpen = false;
            container.SetActive(false);
            UpdatePlaceholder(new StringProps(value, value.ToString()));
        }
        
        public void InitElements()
        {
            items.Clear();
            for (var i = 1; i < maxValue / 5 + 1; i++)
            {
                var value = i * 5;
                var item = new StringProps(value, value.ToString());
                items.Add(item);
            }
        }

        public void UpValue()
        {
            if (currentValue == maxValue) return;
            UpdateCurrentValue(currentValue + 1);
        }

        public void DownValue()
        {
            if (currentValue <= 1) return;
            UpdateCurrentValue(currentValue - 1);
        }

    }
}