using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Utils
{
    public class DynamicText : MonoBehaviour
    {
        [SerializeField] private ContentSizeFitter fitter;
        [SerializeField] private float maxWidth = 256;
        [SerializeField] private TextMeshProUGUI textMesh;

        public RectTransform RectTransform => textMesh.rectTransform;

        public IEnumerator UpdateText(string text)
        {
            textMesh.text = text;
            yield return new WaitForEndOfFrame();
            CheckBounds();
        }

        public void CheckBounds()
        {
            if (textMesh.rectTransform.sizeDelta.x > maxWidth)
            {
                fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                textMesh.rectTransform.sizeDelta = new Vector2(maxWidth, textMesh.rectTransform.sizeDelta.y);
            }
        }
    }
}
