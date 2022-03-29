using TMPro;
using UIScripts.CommonComponents.Popup;
using UnityEngine;

namespace UIScripts.Utils
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private BetterPopup popup;
        [SerializeField] private TextMeshProUGUI textPrefab;

        public void Show(Vector2 position, string text)
        {
            popup.Clear();
            popup.gameObject.SetActive(true);
            var textMesh = Instantiate(textPrefab);
            textMesh.text = text;
            StartCoroutine(popup.AddItemWithFitter(textMesh.rectTransform));

            popup.RectTransform.position = position;
        }

        public void Move(Vector2 position) => popup.RectTransform.position = position;

        public void Hide() => popup.gameObject.SetActive(false);

    }
}