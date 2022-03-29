using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIScripts
{
    public abstract class FixedSizeList<T> : MonoBehaviour
    {
        public List<T> itemsList;
        protected GameObject ItemPrefab => itemPrefab;
        protected Transform ItemsContainer => itemsContainer;
        
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] protected Transform itemsContainer;
        [SerializeField] private int visibleListSize;
        [SerializeField] private StyledIconButton upButton;
        [SerializeField] private StyledIconButton downButton;
        [SerializeField] private float itemHeight;
        [SerializeField] private RectMask2D mask;

        public int VisibleListSize => visibleListSize;

        [SerializeField] private List<UIElement> uiElements;

        private int _currentPosition;
        private VerticalLayoutGroup _layout;

        protected int CurrentPosition => _currentPosition;

        protected abstract void InitItem(T props);

        private void Awake()
        {
            Initialize();

            CheckButtonActivity();
        }

        public void CheckButtonActivity()
        {
            if (CurrentPosition == 0)
            {
                upButton.SetDisabled(true);
                downButton.SetDisabled(false);
            } else if (CurrentPosition == itemsList.Count - VisibleListSize)
            {
                upButton.SetDisabled(false);
                downButton.SetDisabled(true);
            }
            else
            {
                upButton.SetDisabled(false);
                downButton.SetDisabled(false);
            }
        }

        public void Initialize()
        {
            _currentPosition = 0;
            if (itemsList.Count == 0) return;
            if (visibleListSize == 0) return;
            if (!itemPrefab) return;
            if (!itemsContainer) return;
            
            if (itemsList.Count < VisibleListSize)
            {
                upButton.gameObject.SetActive(false);
                downButton.gameObject.SetActive(false);
            }
            
            itemsList.ForEach(InitItem);
            uiElements.ForEach(el => el.UpdateUI());
            
            if (!_layout) _layout = itemsContainer.GetComponent<VerticalLayoutGroup>();
            itemHeight = itemPrefab.GetComponent<RectTransform>().sizeDelta.y;
            var maskRect = mask.GetComponent<RectTransform>();
            var calculatedHeight = visibleListSize * (itemHeight + _layout.spacing) + _layout.padding.vertical;
            maskRect.sizeDelta = new Vector2(maskRect.sizeDelta.x, calculatedHeight);
        }

        public void SwapPosition(int pos)
        {
            if (pos < 0) return;
            if (pos + visibleListSize > itemsList.Count) return;
        
            _currentPosition = pos;
            var rect = itemsContainer.GetComponent<RectTransform>();
            if (!_layout) _layout = itemsContainer.GetComponent<VerticalLayoutGroup>();
            rect.anchoredPosition = 
                new Vector2(rect.anchoredPosition.x, _currentPosition * (itemHeight + _layout.spacing));
            uiElements.ForEach(el => el.UpdateUI());
            CheckButtonActivity();
        }

        public void UpItems() => SwapPosition(_currentPosition - 1);
    
        public void DownItems() => SwapPosition(_currentPosition + 1);
        
    }
}