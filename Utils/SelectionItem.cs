using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class SelectionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Serializable]
        private class Style
        {
            public Sprite icon;
            public UIColorStyle backgroundColor;
            public UIColorStyle borderColor;
        }

        [SerializeField] private Image iconImage;
        [SerializeField] private StyledImage border;

        public Image IconImage => iconImage;
        public StyledImage Border => border;

        [SerializeField] private UIState<Style> styles;
        
        [SerializeField] private UIGradient gradient;
        [SerializeField] private UIColorStyle gradientColor;

        public Toggle Toggle
        {
            get
            {
                if (!_toggle) _toggle = GetComponent<Toggle>();
                return _toggle;
            }
        }

        private Style _currentStyle;
        private StyledImage _styledImage;
        private Toggle _toggle;
        private bool _isOver = false;

        private void Awake()
        {
            if (!_toggle) _toggle = GetComponent<Toggle>();
            if (!_styledImage) _styledImage = GetComponent<StyledImage>();
            Initialize();
            SwapStyle(_toggle.isOn ? styles.pressed : styles.common);
        }

        public void SetSprites(Sprite common, Sprite selected)
        {
            styles.common.icon = common;
            styles.hover.icon = common;
            styles.pressed.icon = selected;
            UpdateUI();
        }
        public void UpdateUI()
        {
            if (!_toggle) _toggle = GetComponent<Toggle>();
            
            if (_currentStyle == null)
                _currentStyle = styles.common;
            
            if (gradient)
                gradient.gameObject.SetActive(_toggle.isOn);
            
            if (border)
                border.SwapColor(_currentStyle.borderColor);

            if (!_styledImage) _styledImage = GetComponent<StyledImage>();
            _styledImage.SwapColor(_currentStyle.backgroundColor);
            
            if (!iconImage) return;
            if (_currentStyle.icon)
            {
                iconImage.overrideSprite = _currentStyle.icon;
            }
        }

        public void Initialize()
        {
            if (!gradient) return;
            var color = UISettings.Instance.colors.Pick(gradientColor);
            gradient.m_color1 = gradient.m_color2 = color;
            gradient.m_color2.a = 0;
            gradient.UpdateUI();
        }

        private void SwapStyle(Style style)
        {
            _currentStyle = style;
            UpdateUI();
        }

        public void ValueChangeHandler(Toggle toggle)
        {
            SwapStyle(toggle.isOn ? styles.pressed : styles.common);
            if (_isOver) 
                UISettings.Instance.GameCursor.SetCursorType(_toggle.isOn ? CursorType.Common : CursorType.Link);
        }
        
        private void OnValidate()
        {
            SwapStyle(styles.common);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isOver = true;
            if (!_toggle) _toggle = GetComponent<Toggle>();
            if (!_toggle.IsInteractable()) return;
            UISettings.Instance.GameCursor.SetCursorType(_toggle.isOn ? CursorType.Common : CursorType.Link);
            if (_toggle.isOn) return;
            if (!_styledImage) _styledImage = GetComponent<StyledImage>();
            SwapStyle(styles.hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isOver = false;
            if (!_toggle) _toggle = GetComponent<Toggle>();
            if (!_toggle.IsInteractable()) return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            if (_toggle.isOn) return;
            if (!_styledImage) _styledImage = GetComponent<StyledImage>();
            SwapStyle(styles.common);
        }
    }
}