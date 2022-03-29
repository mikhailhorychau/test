using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents
{
    public class ObjectMousePositionMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action onPointerEnter;
        public event Action onPointerExit;
        
        [SerializeField] private RectTransform popup;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private Vector2 padding = new Vector2(10f, 10f);

        private Vector2 _prevMousePosition;

        // public UnityEvent onPointerEnter;
        // public UnityEvent onPointerExit;

        public RectTransform Popup
        {
            get => popup;
            set => popup = value;
        }

        public RectTransform MoveContainer
        {
            get => moveContainer;
            set => moveContainer = value;
        }

        private void Awake()
        {
            // _camera = Camera.current;
        }

        private void Update()
        {
            if (!popup) return;
            if (popup.gameObject.activeSelf && !Input.mousePosition.Equals(_prevMousePosition))
                MovePopup();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!popup) return;
            onPointerEnter?.Invoke();
            popup.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!popup) return;
            onPointerExit?.Invoke();
            popup.gameObject.SetActive(false);
        }

        private void MovePopup()
        {
            _prevMousePosition = Input.mousePosition;
            popup.localPosition = NormalizePosition(_prevMousePosition);
        }

        private Vector2 NormalizePosition(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                moveContainer,
                position,
                null,
                out var localPosition
            );

            var normalized = new Vector2(localPosition.x + padding.x, localPosition.y + padding.y);
            if (localPosition.x + popup.rect.width > moveContainer.rect.xMax)
            {
                normalized.x = moveContainer.rect.xMax - popup.rect.width - padding.x;
            }

            if (localPosition.y + popup.rect.height > moveContainer.rect.xMax)
            {
                normalized.y = moveContainer.rect.yMax - popup.rect.height - padding.y;
            }

            return normalized;
        }
    }
}