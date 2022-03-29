using System;
using TMPro;
using UIScripts.CommonComponents;
using UIScripts.CommonComponents.DropdownButton.ActionButton;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.EngineProfile
{
    public class EngineProfileUI : MonoBehaviour
    {
        [SerializeField] private Image logo;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private SingleRequirementButtonPresenter reqBtn;
        [SerializeField] private EngineProfileParams engineParams;
        [SerializeField] private ActionDropdownButton actionBtn;
        [SerializeField] private ContractsContainer currentContracts;
        [SerializeField] private ContractsContainer nextYearContracts;
        [SerializeField] private RequirementConfirmationPopup confirmationPopup;

        public Sprite Logo
        {
            set => logo.overrideSprite = value;
        }

        public string Title
        {
            set => title.text = value;
        }

        public SingleRequirementButtonPresenter ReqBtn => reqBtn;
        public ActionDropdownButton ActionBtn => actionBtn;
        public EngineProfileParams EngineParams => engineParams;
        public ContractsContainer CurrentContracts => currentContracts;
        public ContractsContainer NextYearContracts => nextYearContracts;
        public RequirementConfirmationPopup ConfirmationPopup => confirmationPopup;
    }

    [Serializable]
    public struct EngineProfileParams
    {
        [SerializeField] private SkillProgressParam power;
        [SerializeField] private SkillProgressParam reliability;
        [SerializeField] private SkillProgressParam heat;
        [SerializeField] private SkillProgressParam fuelEconomy;

        public SkillProgressParam Power => power;
        public SkillProgressParam Reliability => reliability;
        public SkillProgressParam Heat => heat;
        public SkillProgressParam FuelEconomy => fuelEconomy;
    }
}