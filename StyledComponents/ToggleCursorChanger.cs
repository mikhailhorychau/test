using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    public class ToggleCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Toggle toggle;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!toggle.isActiveAndEnabled)
                return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Link);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!toggle.isActiveAndEnabled)
                return;
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
        }
    }
}