using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.Popup
{
    public enum ToolTipType
    {
        Following,
        Opening
    }
    
    public class ToolTipPopup : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform popup;
        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private TextMeshProUGUI infoDescription;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Vector2 padding;
        [SerializeField] private float disappearTime;
        [SerializeField] private ToolTipType type;

        private bool _isOpen = false;
        private GameObject _current = null;
        
        private Vector2 _prevPosition = Vector2.one;

        private IEnumerator _autoHideRoutine;

        public string Description
        {
            set => infoDescription.text = value;
        }

        public void Hide()
        {
            if (!gameObject.activeInHierarchy || !gameObject.activeSelf)
                return;
            
            if (_autoHideRoutine != null)
                StopCoroutine(_autoHideRoutine);

            _isOpen = false;
            gameObject.SetActive(false);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClickHandler(eventData, _current, "");
        }

        public void PointerClickHandler(PointerEventData eventData, GameObject obj, string description)
        {
            if (type == ToolTipType.Following) return;
            
            if (_current == obj)
                _isOpen = !_isOpen;
            else
            {
                _current = obj;
                _isOpen = true;
                Description = description;
            }
            
            if (_autoHideRoutine != null)
                StopCoroutine(_autoHideRoutine);
            
            popup.gameObject.SetActive(_isOpen);
            canvasGroup.alpha = 0f;

            if (_isOpen)
            {
                _autoHideRoutine = AutoHideRoutine();
                StartCoroutine(_autoHideRoutine);

                StartCoroutine(ShowRoutine(eventData.position));
            }
        }

        public void PointerEnterHandler(PointerEventData eventData, string description = "")
        {
            if (type == ToolTipType.Following)
            {
                Description = description;
                popup.localPosition = NormalizePosition(eventData.position);
                popup.gameObject.SetActive(true);
                _isOpen = true;
                return;
            }
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Info);
        }

        public void PointerExitHandler(PointerEventData eventData)
        {
            if (type == ToolTipType.Following)
            {
                popup.gameObject.SetActive(false);
                _isOpen = false;
                return;
            }
            
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }

        private void Update()
        {
            if (_isOpen && type == ToolTipType.Following)
            {
                var position = Input.mousePosition;
                if (_prevPosition.Equals(position))
                    return;
                popup.localPosition = NormalizePosition(position);
                _prevPosition = position;
            }
        }

        private IEnumerator ShowRoutine(Vector2 position)
        {
            yield return new WaitForEndOfFrame();
            popup.localPosition = NormalizePosition(position);
            canvasGroup.alpha = 1f;
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
            
            if (localPosition.x + container.rect.width > moveContainer.rect.xMax)
            {
                normalized.x = moveContainer.rect.xMax - container.rect.width - padding.x;
            }

            if (localPosition.y + container.rect.height > moveContainer.rect.xMax)
            {
                normalized.y = moveContainer.rect.yMax - container.rect.height - padding.y;
            }
            

            return normalized;
        }

        private IEnumerator AutoHideRoutine()
        {
            yield return new WaitForSeconds(disappearTime);
            _isOpen = false;
            popup.gameObject.SetActive(false);
        }
    }
}