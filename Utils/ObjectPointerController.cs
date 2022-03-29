using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UIScripts
{
    public class ObjectPointerController : 
        MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler
    {
        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;
        public UnityEvent onPointerClick;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick.Invoke();
        }
    }
}