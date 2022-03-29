using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class Popup : MonoBehaviour
    {
        [TextArea(5, 8)]
        [SerializeField] public string headerText;

        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform header;
        [SerializeField] private RectTransform body;
        [SerializeField] private TextMeshProUGUI headerTextComponent;

        public RectTransform rectTransform;

        private Vector2 _cachedSize;

        public Transform Body => body.transform;

        private void Awake()
        {
            if (!rectTransform) rectTransform = GetComponent<RectTransform>();
            Initialize();
        }
        
        public void Initialize()
        {
            headerTextComponent.text = headerText;
            header.gameObject.SetActive(headerText.Length != 0);
            Invoke(nameof(Resize), Time.deltaTime);
        }

        public void ClearBody()
        {
            foreach (Transform tr in Body)
            {
                Destroy(tr.gameObject);
            }
        }
        
        public void Resize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(header);
            LayoutRebuilder.ForceRebuildLayoutImmediate(body);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            if (!rectTransform) rectTransform = GetComponent<RectTransform>();
            var sizeDelta = content.sizeDelta;
            if (_cachedSize == sizeDelta) return;

            _cachedSize = sizeDelta;
            rectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
        }
    }
}