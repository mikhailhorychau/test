using System;
using TMPro;
using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.RepairAndUpgrade
{
    public class RepairAndUpgradeTotalPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalTitle;
        [SerializeField] private TextMeshProUGUI deadlineTitle;
        [SerializeField] private TextMeshProUGUI deadlineValue;
        [SerializeField] private RequirementButton reqButton;
        [SerializeField] private StaticPopup popup;
        
        private RepairAndUpgradeTotalModel _model;

        public event Action OnStart;

        private void Awake()
        {
            reqButton.OnButtonClick += ReqBtnClickListener;
        }

        public void Initialize(RepairAndUpgradeTotalModel model)
        {
            RemoveListeners();
            _model = model;

            totalTitle.text = model.TotalTitle;
            deadlineTitle.text = model.DeadlineTitle;
            deadlineValue.text = model.DeadlineValue;

            SetRequirementsDone(model.RequirementsButtonData.RequirementsDone);
            reqButton.InitializeRequirements(model.RequirementsButtonData.Requirements);

            AddListeners();
        }

        private void AddListeners()
        {
            if (_model != null)
            {
                _model.RequirementsButtonData.RequirementsDone.OnValueChange += SetRequirementsDone;
                _model.DeadlineValue.OnValueChange += SetDeadlineValue;
                _model.NothingSelected.OnValueChange += NothingSelectedListener;
            }
        }

        private void RemoveListeners()
        {
            if (_model != null)
            {
                _model.RequirementsButtonData.RequirementsDone.OnValueChange -= SetRequirementsDone;
                _model.DeadlineValue.OnValueChange -= SetDeadlineValue;
                _model.NothingSelected.OnValueChange -= NothingSelectedListener;
            }
        }

        private void SetRequirementsDone(bool isDone)
        {
            UpdateButtonTitle();

            reqButton.RequirementsDone = isDone;
        }
        private void SetDeadlineValue(string value) => deadlineValue.text = value;

        private void ReqBtnClickListener()
        {
            popup.Title = _model.ConfirmationTitle;
            popup.gameObject.SetActive(true);
            popup.onAccept.AddListener(PopupAcceptListener);
        }

        private void PopupAcceptListener()
        {
            OnStart?.Invoke();
        }

        private void NothingSelectedListener(bool value) => UpdateButtonTitle();
        private void UpdateButtonTitle() => reqButton.Title = GetButtonTitle();

        private string GetButtonTitle()
        {
            var reqData = _model.RequirementsButtonData;
            if (_model.NothingSelected)
                return _model.NothingSelectedTitle;

            return reqData.RequirementsDone ? reqData.Title : reqData.ReqNotDoneTitle;
        }

        private void OnDestroy()
        {
            RemoveListeners();
            reqButton.OnButtonClick -= ReqBtnClickListener;
        }
    }

    public class RepairAndUpgradeTotalModel
    {
        public string TotalTitle { get; set; }
        public string DeadlineTitle { get; set; }
        public string ConfirmationTitle { get; set; }
        
        public string NothingSelectedTitle { get; set; }
        public ObservableBool NothingSelected { get; } = new ObservableBool();
        public ObservableString DeadlineValue { get; } = new ObservableString();
        public RequirementButtonData RequirementsButtonData { get; } = new RequirementButtonData();
    }
}