using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts
{
    public abstract class StyledDropdown2<TProps, TPresenter> : 
        DropdownBase<TProps, TPresenter> 
        where TPresenter : IDropdownItemPresenter<TProps> 
        where TProps : IProps
    {
        [Serializable]
        private class DropdownStyle
        {
            public UIColorStyle borderColor;
            public Sprite cornerSprite;
        }

        [Serializable]
        private class DropdownStyles
        {
            public DropdownStyle common;
            public DropdownStyle hover;
            public DropdownStyle selected;
        }

        [SerializeField] private DropdownStyles dropdownStyles;

        [SerializeField] private StyledImage divider;
        [SerializeField] private StyledImage corner;

        private void UpdateUI(DropdownStyle dropdownStyle)
        {
            if (!divider) divider = transform.Find("Divider").GetComponent<StyledImage>();
            if (!corner) corner = transform.Find("Corner").GetComponent<StyledImage>();
        
            divider.colorStyle = dropdownStyle.borderColor;
            divider.UpdateUI();
            corner.colorStyle = dropdownStyle.borderColor;
            corner.image.sprite = dropdownStyle.cornerSprite;
            corner.UpdateUI();
        }

        public void UpdateUI() => UpdateUI(dropdownStyles.common);
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (isDisabled) return;
            base.OnPointerEnter(eventData);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            if (isOpen) return;
            UpdateUI(dropdownStyles.hover);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (isDisabled) return;
            base.OnPointerExit(eventData);
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            if (isOpen) return;
            UpdateUI(dropdownStyles.common);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            UpdateUI(isOpen ? dropdownStyles.selected : dropdownStyles.common);
        }

        public override void Deselect()
        {
            base.Deselect();
            UpdateUI(dropdownStyles.common);
        }
    }
}