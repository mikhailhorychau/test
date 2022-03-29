using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class RequirementConfirmationPopup : MonoBehaviour
    {
        [SerializeField] private StaticPopup popup;
        [SerializeField] private RequirementsButtonPresenter reqBtn;

        public StaticPopup Popup => popup;
        public RequirementsButtonPresenter ReqBtn => reqBtn;
    }
}