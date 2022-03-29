using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIScripts
{
    [RequireComponent(typeof(ObjectPointerController))]
    public abstract class StyledDropdownItem2<TProps> : 
        MonoBehaviour, 
        IDropdownItemPresenter<TProps>
    {
        
        [SerializeField] private UIColorStyle commonStyle;
        [SerializeField] private UIColorStyle hoverStyle;
        [SerializeField] public Image target;
        [SerializeField] private bool isPlaceholderPresenter;

        protected ObjectPointerController controller;

        private void OnEnable()
        {
            if (isPlaceholderPresenter) return;
            if (!controller) controller = GetComponent<ObjectPointerController>();
            controller.onPointerEnter.AddListener(OnEnter);
            controller.onPointerExit.AddListener(OnExit);
            controller.onPointerClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            if (isPlaceholderPresenter) return;
            controller.onPointerEnter.RemoveListener(OnEnter);
            controller.onPointerExit.RemoveListener(OnExit);
            controller.onPointerClick.RemoveListener(OnClick);
        }

        public virtual void Initialize(TProps props)
        {
            if (!controller) controller = GetComponent<ObjectPointerController>();

            UpdateUI();
        }

        public void UpdateUI()
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }

        public void OnEnter()
        {
            target.color = UISettings.Instance.colors.Pick(hoverStyle);
        }

        public void OnExit()
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }

        public void OnClick()
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }

        public void OnDeselect()
        {
            target.color = UISettings.Instance.colors.Pick(commonStyle);
        }
    }
}