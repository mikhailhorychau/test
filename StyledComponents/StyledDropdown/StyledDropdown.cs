using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class StyledDropdown : 
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler,
        IPointerUpHandler,
        IDeselectHandler
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
        [SerializeField] private TextStyle commonFontStyle;
        [SerializeField] private TextStyle selectedFontStyle;
    
        [SerializeField] private StyledImage divider;
        [SerializeField] private StyledImage corner;
        [SerializeField] private TextMeshProUGUI textPlaceholder;
        [SerializeField] private GameObject contentContainer;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject itemPrefab;

        public string[] values;

        public string title;

        private bool _isOpen;
        private int _value = -1;
        private bool _mouseIsOver;

        private Transform _content;

        private void Start()
        {
            UpdateUI(dropdownStyles.common);
            UpdateValue(-1, title);
            InitItems();
        }

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

        private void OnValidate()
        {
            UpdateUI(dropdownStyles.common);
            UpdateValue(-1, title);
        }

        public void UpdateValue(int value, string text)
        {
            _value = value;
            textPlaceholder.text = text;
            var styledText = textPlaceholder.gameObject.GetComponent<StyledText>();

            styledText.textStyle = value != -1 ? selectedFontStyle : commonFontStyle; 
            styledText.UpdateUI();

            _isOpen = false;
            contentContainer.SetActive(false); 
        }

        protected void InitItems()
        {
            var index = 0;
            foreach (var value in values)
            {
                var itemObj = Instantiate(itemPrefab, content.transform);
                itemObj.GetComponentInChildren<TextMeshProUGUI>().text = value;
            
                var index1 = index;
                itemObj.GetComponent<StyledDropdownItem>().onClick.AddListener(() => UpdateValue(index1, value));

                var contentWidth = content.GetComponent<RectTransform>().rect.width;
                var itemRect = itemObj.GetComponent<RectTransform>();
                itemRect.sizeDelta = new Vector2(contentWidth, itemRect.sizeDelta.y);
                index++;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseIsOver = true;
            if (_isOpen) return;
            UpdateUI(dropdownStyles.hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseIsOver = false;
            if (_isOpen) return;
            UpdateUI(dropdownStyles.common);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPress.GetComponent<Scrollbar>() != null) return;
            _isOpen = !_isOpen;
            
            UpdateUI(_isOpen ? dropdownStyles.selected : dropdownStyles.common);
        
            if (!content) content = transform.Find("Content").gameObject;
            if (!content) return;
        
            contentContainer.SetActive(_isOpen);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (_mouseIsOver) return;
            Deselect();
        }

        private void Deselect()
        {
            _isOpen = false;
            UpdateUI(dropdownStyles.common);

            if (!content) content = transform.Find("Content").gameObject;
            if (!content) return;
            contentContainer.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}