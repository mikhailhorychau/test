using System.Collections;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents.Popup
{
    public class BetterPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        public RectTransform RectTransform => rectTransform;

        [SerializeField] private Transform container;
        [SerializeField] private RectOffset padding;
        [SerializeField] private VerticalLayoutGroup layout;
        [SerializeField] private float spacing;
        
        private VerticalLayoutGroup _layout;
        private Vector2 _startRectSize;
        private bool _isEmpty;
        private Canvas _canvas;

        private void Start()
        {
            _layout = container.GetComponent<VerticalLayoutGroup>();
            _layout.padding = padding;
            _layout.spacing = spacing;
            _startRectSize = new Vector2(0, padding.top + padding.bottom);
        }

        public void AddItem(RectTransform item)
        {
            item.transform.SetParent(container);
            rectTransform.sizeDelta = CalculateNewSize(rectTransform.sizeDelta, item.sizeDelta, _isEmpty ? 0 : spacing);
            item.localScale = Vector3.one;
            _isEmpty = false;
        }

        public IEnumerator AddItemWithFitter(RectTransform item)
        {
            yield return new WaitForEndOfFrame();
            AddItem(item);
        }

        public void Clear()
        {
            rectTransform.sizeDelta = new Vector2(0, padding.top + padding.bottom);
            container.Clear();
            _isEmpty = true;
        }

        private Vector2 CalculateNewSize(Vector2 prev, Vector2 item, float additionalSpacing = 0) => 
            new Vector2(
                Mathf.Max(prev.x, item.x + padding.left + padding.right), 
                prev.y + item.y + additionalSpacing);
    }
}