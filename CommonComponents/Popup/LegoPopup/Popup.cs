using UnityEngine;

namespace UIScripts.CommonComponents.Popup.LegoPopup
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private StyledImage background;
        [SerializeField] private StyledImage border;

        public UIColorStyle BackgroundColor
        {
            get => background.colorStyle;
            set => background.SwapColor(value);
        }

        public UIColorStyle BorderColor
        {
            get => border.colorStyle;
            set => border.SwapColor(value);
        }

        public void AddContent(GameObject obj) => obj.transform.SetParent(rect);
    }
}