using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.SpriteLoader
{
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteHandler<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private Image image;
        [SerializeField] private T type;

        private Image Image
        {
            get
            {
                if (image == null)
                    image = gameObject.GetComponent<Image>();

                return image;
            }
        }

        public void SetSprite(T spriteType)
        {
            type = spriteType;
            UpdateUI();
        }

        private void UpdateUI()
        {
            var sprite = App.runtime.SpriteSelector.GetSprite(type);
            
            if (sprite == Image.sprite) 
                return;
            
            Image.overrideSprite = sprite;
            Image.sprite = sprite;
        }

        private void OnValidate()
        {
            UpdateUI();
        }
    }
}