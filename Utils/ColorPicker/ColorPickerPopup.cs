using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.Utils.ColorPicker
{
    public class ColorPickerPopup : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private TextMeshProUGUI chooseColorTitle;
        [SerializeField] private TextMeshProUGUI hexColorTitle;
        [SerializeField] private TextMeshProUGUI applyTitle;
        [SerializeField] private TextMeshProUGUI cancelTitle;

        public string ChooseColorTitle
        {
            get => chooseColorTitle.text;
            set => chooseColorTitle.text = value;
        }

        public string HexColorTitle
        {
            get => hexColorTitle.text;
            set => hexColorTitle.text = value;
        }

        public string ApplyTitle
        {
            get => applyTitle.text;
            set => applyTitle.text = value;
        }

        public string CancelTitle
        {
            get => cancelTitle.text;
            set => cancelTitle.text = value;
        }

        [SerializeField] private HSVPicker.ColorPicker hsvPicker;
        public HSVPicker.ColorPicker HSVPicker => hsvPicker;

        [SerializeField] private Image currentColorImage;

        private RectTransform _currentColorRect;

        [SerializeField] private StyledButton applyBtn;
        [SerializeField] private StyledButton cancelBtn;

        [SerializeField] private RectTransform popupRect;
        [SerializeField] private RectTransform headerRect;
        [SerializeField] private RectTransform moveContainer;

        public UnityEvent<Color> onApplyColor;
        
        public UnityEvent onCancel;

        private bool _isDrag;
        
        private void Awake()
        {
            _currentColorRect = currentColorImage.GetComponent<RectTransform>();
            applyBtn.onClick.AddListener(() =>
            {
                onApplyColor.Invoke(hsvPicker.CurrentColor);
                gameObject.SetActive(false);
            });
            cancelBtn.onClick.AddListener(() =>
            {
                onCancel.Invoke();
                gameObject.SetActive(false);
            });
        }

        public void Initialize(Color color)
        {
            // onApplyColor.RemoveAllListeners();
            // onCancel.RemoveAllListeners();
            
            currentColorImage.color = color;
            hsvPicker.CurrentColor = color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_currentColorRect, Input.mousePosition))
            {
                hsvPicker.CurrentColor = currentColorImage.color;
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDrag /*&& RectTransformUtility.RectangleContainsScreenPoint(popupRect, Input.mousePosition)*/)
            {
                var position = popupRect.anchoredPosition + eventData.delta;

                if (position.x < 0)
                    position.x = 0;
                
                if (position.y > 0)
                    position.y = 0;
                
                if (position.x + popupRect.rect.width > moveContainer.rect.width)
                    position.x = moveContainer.rect.width - popupRect.rect.width;
                
                if (position.y - popupRect.rect.height < -moveContainer.rect.height)
                    position.y = popupRect.rect.height - moveContainer.rect.height;
                
                popupRect.anchoredPosition = position;
                
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(headerRect, Input.mousePosition))
                _isDrag = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
        }
    }
}