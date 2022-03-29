using TMPro;
using UIScripts.CommonComponents.Popup;
using UnityEngine;
using UnityEngine.Events;

namespace UIScripts.Utils.DropdownSelection
{
    public class DropdownSelectionItem : MonoBehaviour
    {
        [SerializeField] private StyledImage styledImage;

        public StyledImage StyledImage => styledImage;
        
        [SerializeField] private TextMeshProUGUI nameTitle;
        [SerializeField] private TextMeshProUGUI infoTitle;
        [SerializeField] private StepDropdown dropdown;
        [SerializeField] private StyledIconButton deleteButton;
        [SerializeField] private TextMeshProUGUI popupTitle;
        [SerializeField] private BetterPopupComponent betterPopupComponent;
        [SerializeField] private DropdownSelectionItemProps itemProps;

        public UnityEvent<int> onDeleteItem;
        public UnityEvent onValueChange;

        public BetterPopupComponent BetterPopupComponent => betterPopupComponent;

        private int _id = -1;
        public int ID => _id;
        
        private void Awake()
        {
            dropdown.onValueChange.AddListener(ValueChangeListener);
            deleteButton.onClick.AddListener(() => onDeleteItem.Invoke(itemProps.ID));
        }

        private void ValueChangeListener(int value)
        {
            itemProps.SelectedID = value;
            onValueChange.Invoke();
            UpdateInfoTitle();
        }

        public void UpdateInfoState(bool selected)
        {
            dropdown.gameObject.SetActive(!selected);
            deleteButton.gameObject.SetActive(!selected);
            infoTitle.gameObject.SetActive(selected);
        }

        public void UpdateInfoTitle()
        {
            infoTitle.text = dropdown.CurrentProps.value;
        }
        
        public void Initialize(DropdownSelectionItemProps initProps, Delegates.DropdownValueConverter converter = null)
        {
            _id = initProps.ID;
            itemProps = initProps;
            nameTitle.text = initProps.Name;
            
            dropdown.Initialize(initProps.MINValue, initProps.MAXValue, initProps.DropdownStep, initProps.ClickStep, converter);
            dropdown.CurrentValue = initProps.SelectedID;
            
            UpdateInfoTitle();
            UpdateWarning();
            betterPopupComponent.onPointerEnter.AddListener(WarningPointerEnterListener);
        }

        public void UpdateWarning(bool withWarning, string text)
        {
            itemProps.WithWarning = withWarning;
            itemProps.WarningMessage = text;
        }
        
        public void UpdateWarning(bool withWarning)
        {
            itemProps.WithWarning = withWarning;
            UpdateWarning();
        }

        public void UpdateWarning()
        {
            betterPopupComponent.gameObject.SetActive(itemProps.WithWarning);
        }

        private void WarningPointerEnterListener()
        {
            betterPopupComponent.Popup.Clear();
            var textMesh = Instantiate(popupTitle);
            textMesh.text = itemProps.WarningMessage;

            StartCoroutine(betterPopupComponent.Popup.AddItemWithFitter(textMesh.rectTransform));
        }
    }
}