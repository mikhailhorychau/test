using System;
using TMPro;
using UIScripts.Screens.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UIScripts.Table.DynamicTable
{
    
    public class DynamicTableHeaderColumn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Serializable]
        public enum HeaderState
        {
            Common,
            TopToBottom,
            BottomToTop
        }
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private GameObject selectionBackground;
        [SerializeField] private GameObject divider;
        [SerializeField] private StyledImage topArrow;
        [SerializeField] private StyledImage bottomArrow;
        [SerializeField] private RectTransform rect;

        [SerializeField] private bool disabled;

        public bool Disabled
        {
            get => disabled;
            set => disabled = value;
        }

        public UnityEvent<SearchHeaderState, int> onStateChange;
        public UnityEvent<int> onSelect;

        private SearchHeaderState _state = SearchHeaderState.Common;
        private int _id;
        private bool _isLast;

        public int ID => _id;

        public void Initialize(string text, float width, bool isLast = false, int id = -1, 
            TextAlignmentOptions alignment = TextAlignmentOptions.Center)
        {
            title.text = text;
            var sizeDelta = new Vector2(width, rect.sizeDelta.y);
            rect.sizeDelta = sizeDelta;
            divider.SetActive(!isLast);
            _id = id;
            _isLast = isLast;
            title.alignment = alignment;
            
            if (alignment == TextAlignmentOptions.Left)
            {
                var margin = title.margin;
                margin.x = 16;
                title.margin = margin;
            }
            else
            {
                var margin = title.margin;
                margin.x = 0;
                title.margin = margin;
            }
        }

        public void SwitchState(SearchHeaderState state)
        {
            switch (state)
            {
                case SearchHeaderState.Common:
                {
                    bottomArrow.gameObject.SetActive(false);
                    topArrow.gameObject.SetActive(false);
                    selectionBackground.SetActive(false);
                    divider.SetActive(!_isLast);
                    _state = state;
                } 
                    break;
                case SearchHeaderState.TopToBottom:
                {
                    bottomArrow.gameObject.SetActive(true);
                    bottomArrow.SetAlpha(1f);
                    topArrow.gameObject.SetActive(false);
                    selectionBackground.SetActive(true);
                    divider.SetActive(false);
                    _state = state;
                } 
                    break;
                case SearchHeaderState.BottomToTop:
                {
                    bottomArrow.gameObject.SetActive(false);
                    topArrow.gameObject.SetActive(true);
                    topArrow.SetAlpha(1f);
                    selectionBackground.gameObject.SetActive(true);
                    _state = state;
                }
                    break;
            }
            if (_state != SearchHeaderState.Common) //
                onStateChange.Invoke(state, _id);
        }
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (disabled) return;
            switch (_state)
            {
                case SearchHeaderState.Common:
                {
                    bottomArrow.gameObject.SetActive(true);
                    topArrow.gameObject.SetActive(false);
                    bottomArrow.SetAlpha(1f);
                } 
                    break;
                case SearchHeaderState.TopToBottom:
                {
                    topArrow.gameObject.SetActive(true);
                    topArrow.SetAlpha(0.5f);
                }
                    break;
                case SearchHeaderState.BottomToTop:
                {
                    bottomArrow.gameObject.SetActive(true);
                    bottomArrow.SetAlpha(0.5f);
                }
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (disabled) return;
            switch (_state)
            {
                case SearchHeaderState.Common:
                {
                    bottomArrow.gameObject.SetActive(false);
                }
                    break;
                case SearchHeaderState.TopToBottom:
                {
                    topArrow.gameObject.SetActive(false);
                }
                    break;
                case SearchHeaderState.BottomToTop:
                {
                    bottomArrow.gameObject.SetActive(false);
                }
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (disabled) return;
            switch (_state)
            {
                case SearchHeaderState.Common:
                {
                    SwitchState(SearchHeaderState.TopToBottom);
                    onSelect.Invoke(_id);
                }
                    break;
                case SearchHeaderState.TopToBottom : SwitchState(SearchHeaderState.BottomToTop);
                    break;
                case SearchHeaderState.BottomToTop : SwitchState(SearchHeaderState.TopToBottom);
                    break;
            }
        }
    }
}