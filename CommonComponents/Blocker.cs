using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class Blocker : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick; 
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }

        public static Blocker Create(Canvas root) => Create(root, root.sortingLayerID, root.sortingOrder - 1);
        public static Blocker CreateWithHighOrder(Canvas root) => Create(root, root.sortingLayerID, 1000);

        public static Blocker Create(Canvas root, int sortingLayerID, int order)
        {
            var obj = new GameObject("Blocker");
            
            var rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.SetParent(root.transform, false);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;

            var canvas = obj.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingLayerID = sortingLayerID;
            canvas.sortingOrder = order;

            obj.AddComponent<GraphicRaycaster>();

            var image = obj.AddComponent<Image>();
            image.color = Color.clear;
            
            var blocker = obj.AddComponent<Blocker>();

            return blocker;
        }
    }
}