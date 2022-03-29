using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class ModalImage : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;

        public Sprite Sprite
        {
            set => image.overrideSprite = value;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }
    }
}