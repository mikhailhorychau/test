using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIScripts.CommonComponents.Popup;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScripts.Utils.DropdownSelection
{
    public class DropdownSelection : MonoBehaviour
    {
        [SerializeField] private DropdownSelectionItem selectionItemPrefab;
        [SerializeField] private RectTransform rootRect;
        [SerializeField] private Transform container;
        [SerializeField] private StyledStringDropdown dropdown;
        [SerializeField] private ZebraList zebra;
        [SerializeField] private BetterPopup popup;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private DropdownSelectionCategoryProps props;

        private List<StyledImage> _images = new List<StyledImage>();
        private List<DropdownSelectionItem> _items = new List<DropdownSelectionItem>();

        private Delegates.DropdownValueConverter _converter;
        
        public UnityEvent onValueChange;
        
        private void Awake()
        {
            dropdown.onValueChange.AddListener(SelectItemListener);
            
            // Initialize(props, null);
        }

        public void Initialize(DropdownSelectionCategoryProps initProps, Delegates.DropdownValueConverter converter)
        {
            _converter = converter;
            props = initProps;
            
            container.Clear();
            _items.Clear();
            _images.Clear();
            
            dropdown.Initialize(GetDropdownItems());
            
            dropdown.PlaceholderText = initProps.PlaceholderText;

            initProps.List
                .FindAll(item => item.IsPresented)
                .ForEach(InitSelectionItem);

            UpdateZebra();
            if (gameObject.activeSelf)
                StartCoroutine(RebuildLayout());
        }

        private void InitSelectionItem(DropdownSelectionItemProps itemProps)
        {
            var selectionItem = Instantiate(selectionItemPrefab, container);
            selectionItem.Initialize(itemProps, _converter);
            selectionItem.onDeleteItem.AddListener(DeleteItemListener);
            selectionItem.onValueChange.AddListener(onValueChange.Invoke);

            selectionItem.BetterPopupComponent.Popup = popup;
            selectionItem.BetterPopupComponent.MoveContainer = moveContainer;
            
            _images.Add(selectionItem.StyledImage);
            _items.Add(selectionItem);
        }
        
        private void SelectItemListener(int id)
        {
            var itemProps = FindItemPropsById(id);
            itemProps.IsPresented = true;
            
            dropdown.Initialize(GetDropdownItems());
            InitSelectionItem(itemProps);
            
            onValueChange.Invoke();
            
            UpdateZebra();
            StartCoroutine(RebuildLayout());
        }

        private void DeleteItemListener(int id)
        {
            var itemProps = FindItemPropsById(id);
            itemProps.IsPresented = false;
            
            dropdown.Initialize(GetDropdownItems());

            var item = FindItemById(id);
            Destroy(item.gameObject);
            
            _images.Remove(item.StyledImage);
            _items.Remove(item);
            
            onValueChange.Invoke();
            
            UpdateZebra();
            StartCoroutine(RebuildLayout());
        }

        public DropdownSelectionItemProps FindItemPropsById(int id) => props.List.Find(item => item.ID == id);

        public DropdownSelectionItem FindItemById(int id) => _items.Find(item => item.ID == id);

        private void UpdateZebra() => zebra.ItemsList = _images;

        public void UpdateInfoState(bool selected)
        {
            _items
                .ForEach(item => item.UpdateInfoState(selected));
            dropdown.gameObject.SetActive(!selected);
        }

        private List<StringProps> GetDropdownItems() =>
            props.List
                .FindAll(item => !item.IsPresented)
                .Select(item => new StringProps(item.ID, item.Name))
                .ToList();

        private IEnumerator RebuildLayout()
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(rootRect);
        }
    }
}