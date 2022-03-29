using UnityEngine;

namespace UIScripts
{
    public class Autosized : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;

        private RectTransform _rectTransform;
        private RectTransform _parentTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentTransform = transform.parent.GetComponent<RectTransform>();
        }

        public void UpdateUI()
        {
            NormalizeValues();
            if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
            if (!_parentTransform) _parentTransform = transform.parent.GetComponent<RectTransform>();

            var rect = _parentTransform.rect;
            var size = new Vector2(rect.width * width / 100, rect.height * height / 100);
            _rectTransform.sizeDelta = size;
            foreach (RectTransform element in transform)
            {
                var el = element.gameObject.GetComponent<Autosized>();
                if (el) el.UpdateUI();
            }
        }

        private void NormalizeValues()
        {
            width = width > 100 ? 100 : width;
            height = height > 100 ? 100 : height;
        }

        private void OnValidate()
        {
            UpdateUI();
        }
    }
}