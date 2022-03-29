using System;
using UIScripts.Utils;
using UnityEngine;

namespace UIScripts
{
    public class StepDropdown : StyledStringDropdown
    {
        private int _minValue = 1000;
        private int _maxValue = 50000;
        private int _dropdownStep = 1000;
        private int _clickStep = 500;

        private Delegates.DropdownValueConverter _converter;
        
        private StringProps _overriden = new StringProps(0, "-");

        public StringProps CurrentProps { get; private set; }

        // private void Start()
        // {
        //     Initialize(_minValue, _maxValue, _dropdownStep, _clickStep);
        // }

        public void Initialize(int minValue, int maxValue, int dropdownStep, int clickStep, 
            Delegates.DropdownValueConverter converter = null, bool isOverridenFirst = false, 
            StringProps overridenFirstProps = null)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _dropdownStep = dropdownStep;
            _clickStep = clickStep;

            if (converter == null)
                _converter = DefaultConverter;
            else
                _converter = converter;
            
            InitDropdownItems(isOverridenFirst);
        }

        private string DefaultConverter(int value) => $"{value} $";

        private void InitDropdownItems(bool isOverridenFirst = false, StringProps overridenFirstProps = null)
        {
            items.Clear();
            // if (isOverridenFirst) 
            //     items.Add(overridenFirstProps ?? _overriden);
            for (var i = _minValue; i <= _maxValue; i += _dropdownStep)
            {
                items.Add(PropsValue(i));
            }
            InitItems();
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
            UpdatePlaceholder(PropsValue(value));
            CurrentProps = PropsValue(value);
            onValueChange.Invoke(value);
        }
        
        private StringProps PropsValue(int value) => new StringProps(value, _converter(value));

        public void UpValue()
        {
            if (currentValue + _clickStep > _maxValue) return;
            UpdateCurrentValue(currentValue + _clickStep);
        }

        public void DownValue()
        {
            if (currentValue - _clickStep < _minValue) return;
            UpdateCurrentValue(currentValue - _clickStep > _maxValue ? _maxValue : currentValue - _clickStep);
        }
    }
}