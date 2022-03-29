using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledTransition : MonoBehaviour
    {
        [SerializeField] private UIColorStyle common;
        [SerializeField] private UIColorStyle hover;
        [SerializeField] private UIColorStyle pressed;
        [SerializeField] private UIColorStyle selected;
        [SerializeField] private UIColorStyle disabled;

        private Selectable _selectable;
        private ColorBlock _colorBlock;
        
        public void UpdateUI()
        {
            if (!_selectable) _selectable = GetComponent<Selectable>();
            if (!_selectable) return;
            var colorBlock = new ColorBlock
            {
                normalColor = UISettings.Instance.colors.Pick(common),
                highlightedColor = UISettings.Instance.colors.Pick(hover),
                pressedColor = UISettings.Instance.colors.Pick(pressed),
                selectedColor = UISettings.Instance.colors.Pick(selected),
                disabledColor = UISettings.Instance.colors.Pick(disabled),
                colorMultiplier = 1
            };
            _selectable.colors = colorBlock;
        }

        private void Awake()
        {
            UpdateUI();
        }
        
        private void OnValidate()
        {
            UpdateUI();
        }
    }
}