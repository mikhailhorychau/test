using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.CommonComponents.DropdownButton.ActionButton
{
    public class ActionDropdownButtonReqItem : ActionDropdownButtonItem
    {
        [SerializeField] private RequirementPresenter requirement;

        public override void Initialize(ActionDropdownButtonData data)
        {
            base.Initialize(data);
            requirement.Initialize(data.Requirement);
        }
    }
}