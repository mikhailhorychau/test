using System;
using UnityEngine;

namespace UIScripts.CommonComponents.DropdownButton.ActionButton
{
    public class ActionDropdownButton : PureDropdown<ActionDropdownButtonItem, ActionDropdownButtonData>
    {
        [SerializeField] protected ActionDropdownButtonItemSubtitle subtitleVariant;
        [SerializeField] protected ActionDropdownButtonReqItem requirementVariant;
        [SerializeField] protected StyledIconButton commonBtn;
        [SerializeField] protected StyledIconButton openBtn;
        
        private ActionDropdownButtonItem GetPrefab(ActionDropdownItemVariant variant)
        {
            return variant switch
            {
                ActionDropdownItemVariant.Common => prefab,
                ActionDropdownItemVariant.Requirement => requirementVariant,
                ActionDropdownItemVariant.Subtitle => subtitleVariant,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override ActionDropdownButtonItem CreateItem(ActionDropdownButtonData data)
        {
            var obj = GetPrefab(data.Variant);
            return Instantiate(obj);
        }

        protected override void OnAwake()
        {
            commonBtn.OnClick.AddListener(Show);
            openBtn.OnClick.AddListener(Hide);
        }

        protected override void OnShow()
        {
            UISettings.Instance.GameCursor.SetCursorType(CursorType.Common);
            commonBtn.gameObject.SetActive(false);
            openBtn.gameObject.SetActive(true);
            
            foreach (var actionDropdownButtonItem in Items)
            {
                actionDropdownButtonItem.TitleMesh.enabled = true;
            }
        }
        
        protected override void OnHide()
        {
            commonBtn.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(false);
        }
    }

    public enum ActionDropdownItemVariant
    {
        Common,
        Subtitle,
        Requirement
    }
}