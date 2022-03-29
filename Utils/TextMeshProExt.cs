using System.Collections;
using TMPro;
using UIScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ExtUI
{
    public class TextMeshProExt : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent<TMP_LinkInfo> onMouseMove;
        public UnityEvent<TMP_LinkInfo> onMouseClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            var link = OnPointerLink(eventData);
            if (link != null)
            {
                onMouseClick.Invoke((TMP_LinkInfo) link);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(nameof(TrackPointer), eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            StopCoroutine(nameof(TrackPointer));
        }

        public IEnumerator TrackPointer(PointerEventData eventData)
        {
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
            var mousePos = Vector3.zero;
            var gameCursor = UISettings.Instance.GameCursor;
            while (Application.isPlaying)
            {
                if (Input.mousePosition != mousePos)
                {
                    gameCursor.SetCursorType(CursorType.Common);
                    mousePos = Input.mousePosition;
                    var link = OnPointerLink(eventData);
                    if (link != null)
                    {
                        gameCursor.SetCursorType(CursorType.Link);
                        onMouseMove?.Invoke((TMP_LinkInfo) link);
                    }
                }

                yield return 0;
            }
        }

        private void OnDestroy()
        {
            onMouseMove.RemoveAllListeners();
            onMouseClick.RemoveAllListeners();
        }

        private TMP_LinkInfo? OnPointerLink(PointerEventData eventData)
        {
            var text = GetComponent<TextMeshProUGUI>();
            var index = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, eventData.pressEventCamera);
            if (index == -1)
            {
                return null;
            }

            return text.textInfo.linkInfo[index];
        }
    }
}