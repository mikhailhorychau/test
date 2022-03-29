using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.Utils.UI
{
    public class ExtendedSelectable : Selectable
    {

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            IsInteractable();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
        }
    }
}