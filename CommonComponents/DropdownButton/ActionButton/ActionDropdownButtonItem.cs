using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts.CommonComponents.DropdownButton.ActionButton
{
    public class ActionDropdownButtonItem : 
        PureDropdownItem<ActionDropdownButtonData>, 
        ISelectHandler,
        IDeselectHandler,
        IPointerClickHandler,
        ISubmitHandler
    {
        [SerializeField] private StyledText title;
        [SerializeField] private StyledImage background;
        private bool _disabled;

        public TextMeshProUGUI TitleMesh => title.TextMesh;

        public override void Initialize(ActionDropdownButtonData data)
        {
            base.Initialize(data);
            Interactable = !data.Disabled;
            title.Text = data.Title;
            var textStyle = data.Disabled ? UIColorStyle.Title : UIColorStyle.Text;
            title.SwapColor(textStyle);
            title.TextMesh.enabled = false;
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (!Interactable) return;
            background.SwapColor(UIColorStyle.Selection);
        }
        
        public void OnDeselect(BaseEventData eventData)
        {
            if (!Interactable) return;
            background.SwapColor(UIColorStyle.Background2);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable)
                return;
            Choose();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (!Interactable)
                return;
            Choose();
        }

        private void Choose()
        {
            if (_disabled) return;
            
            RaiseEvent();
        }
    }
}