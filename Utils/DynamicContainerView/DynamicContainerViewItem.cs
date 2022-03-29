using UnityEngine;

namespace UIScripts.Utils.RecycleStepTable
{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicContainerViewItem : MonoBehaviour
    {
        public float Height => RectTransform.rect.height;
        protected RectTransform _rectTransform;

        protected RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        public void SetPosition(Vector2 position) => RectTransform.anchoredPosition = position;
    }
}