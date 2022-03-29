using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledCheckboxSlider : 
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
    {
        [Serializable]
        private class IndicatorStyle
        {
            public UIColorStyle common;
            public UIColorStyle hover;
            public Sprite sprite;
        }

        [SerializeField] private IndicatorStyle left;
        [SerializeField] private IndicatorStyle right;

        [SerializeField] private GameObject indicator;
        public bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                UpdateUI();
            }
        }

        public UnityEvent onValueChange;
        

        private IndicatorStyle _currentStyle;
        private StyledImage _indicatorStyledImage;

        private void UpdateUI(UIColorStyle colorStyle)
        {
            if (!_indicatorStyledImage) _indicatorStyledImage = indicator.GetComponent<StyledImage>();
            if (!_indicatorStyledImage) return;

            _indicatorStyledImage.image.overrideSprite = _currentStyle.sprite;
            _indicatorStyledImage.colorStyle = colorStyle;
            _indicatorStyledImage.UpdateUI();
        }

        private void Awake()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            _currentStyle = selected ? left : right;
            UpdateUI(_currentStyle.common);
        }

        private void OnValidate()
        {
            UpdateUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UpdateUI(_currentStyle.hover);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UpdateUI(_currentStyle.common);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            selected = !selected;
            UpdateUI();
            onValueChange.Invoke();
        }
    }
}