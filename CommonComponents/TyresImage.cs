using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class TyresImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TyresType type;

        public TyresType Type
        {
            get => type;
            set
            {
                type = value;
                UpdateImage();
            }
        }

        public void UpdateImage()
        {
            if (!image) image = GetComponent<Image>();
            if (!image) return;
            var sprite = UISettings.Instance.tyres.Pick(type);
            image.overrideSprite = sprite;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateImage();
        }
#endif
    }
}