using UnityEngine;

namespace UIScripts.Tutorial
{
    [RequireComponent(typeof(RectTransform))]
    public class TutorialSelection : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        private RectTransform Rect => rectTransform ??= GetComponent<RectTransform>();
        
        private void OnTransformParentChanged()
        {
            const float padding = -5f;
            Rect.offsetMin = new Vector2(padding, padding);
            Rect.offsetMax = new Vector2(-padding, -padding);
            Rect.localScale = Vector3.one;
        }
    }
}