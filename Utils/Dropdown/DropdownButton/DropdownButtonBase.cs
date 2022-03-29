using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts
{
    public abstract class DropdownButtonBase<TProps, TPresenter> : MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerDownHandler, 
        IPointerUpHandler
        where TProps : IProps
        where TPresenter : IDropdownItemPresenter<TProps>
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform container;

        [SerializeField] protected List<TProps> props = new List<TProps>();

        public List<TProps> Props
        {
            get => props;
            set
            {
                if (value == null) return;
                props = value;
                Initialize();
            }
        }

        protected bool isMouseOver;
        protected bool isOpen;

        protected void Initialize()
        {
            if (props.Count == 0) return;

            foreach (Transform tr in container)
            {
                Destroy(tr.gameObject);
            }
            
            props.ForEach(item =>
            {
                var obj = Instantiate(itemPrefab, container);
                var presenter = obj.GetComponent<TPresenter>();
                presenter.Initialize(item);
            });
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isMouseOver) isOpen = !isOpen;
            container.gameObject.SetActive(true);
        }
    }
}