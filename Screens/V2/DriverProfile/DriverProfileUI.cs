using System;
using UIScripts.CommonComponents;
using UIScripts.CommonComponents.DropdownButton.ActionButton;
using UIScripts.Screens.Profile.DriverProfile.Contract;
using UIScripts.Screens.Profile.DriverProfile.Info;
using UIScripts.Screens.Profile.DriverProfile.SeasonResults;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.DriverProfile
{
    public class DriverProfileUI : MonoBehaviour
    {
        [SerializeField] private DriverProfileInfoClass info;
        [SerializeField] private ActionDropdownButton actionBtn;
        [SerializeField] private SingleRequirementButtonPresenter reqBtn;
        [SerializeField] private DriverProfileContractClass contract;
        [SerializeField] private DriverProfileSeasonResultListClass result;
        [SerializeField] private DriverParams driverParams;
        [SerializeField] private RequirementConfirmationPopup confirmationPopup;
        [SerializeField] private PopupWithInput inputPopup;

        public DriverProfileInfoClass Info => info;

        public ActionDropdownButton ActionBtn => actionBtn;

        public SingleRequirementButtonPresenter ReqBtn => reqBtn;

        public DriverProfileContractClass Contract => contract;

        public DriverProfileSeasonResultListClass Result => result;

        public DriverParams DriverParams => driverParams;

        public RequirementConfirmationPopup ConfirmationPopup => confirmationPopup;

        public PopupWithInput InputPopup => inputPopup;
    }

    [Serializable]
    public class DriverParams
    {
        [SerializeField] private SkillProgressParam speed;
        [SerializeField] private SkillProgressParam overtaking;
        [SerializeField] private SkillProgressParam workWithTyres;
        [SerializeField] private SkillProgressParam holdingPosition;
        [SerializeField] private SkillProgressParam stability;
        [SerializeField] private SkillProgressParam aggression;
        [SerializeField] private SkillProgressParam concentration;
        [SerializeField] private SkillProgressParam feedback;
        [SerializeField] private SkillProgressParam wetWeather;

        public SkillProgressParam Speed => speed;

        public SkillProgressParam Overtaking => overtaking;

        public SkillProgressParam WorkWithTyres => workWithTyres;

        public SkillProgressParam HoldingPosition => holdingPosition;

        public SkillProgressParam Stability => stability;

        public SkillProgressParam Aggression => aggression;

        public SkillProgressParam Concentration => concentration;

        public SkillProgressParam Feedback => feedback;

        public SkillProgressParam WetWeather => wetWeather;
    }
}