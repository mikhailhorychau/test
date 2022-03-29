using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.Popup
{
    public class BetterPopupComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BetterPopup popup;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private Vector2 padding = new Vector2(10f, 10f);

        private Vector2 _prevMousePosition;
        private Camera _camera;

        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;

        public BetterPopup Popup
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
            _camera = Camera.current;
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
            popup.gameObject.SetActive(true);
            onPointerEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!popup) return;
            popup.gameObject.SetActive(false);
            onPointerExit.Invoke();
        }

        private void MovePopup()
        {
            _prevMousePosition = Input.mousePosition;
            popup.RectTransform.localPosition = NormalizePosition(_prevMousePosition);
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
            if (localPosition.x + popup.RectTransform.rect.width > moveContainer.rect.xMax)
            {
                normalized.x = moveContainer.rect.xMax - popup.RectTransform.rect.width - padding.x;
            }

            if (localPosition.y + popup.RectTransform.rect.height > moveContainer.rect.xMax)
            {
                normalized.y = moveContainer.rect.yMax - popup.RectTransform.rect.height - padding.y;
            }

            return normalized;
        }
    }
}