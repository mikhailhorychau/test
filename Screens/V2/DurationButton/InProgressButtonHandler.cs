using TMPro;
using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;
using UnityEngine;

namespace UIScripts.Screens.V2.DurationButton
{
    public class InProgressButtonHandler : InProgressHandler
    {
        [SerializeField] private SingleRequirementButton button;
        [SerializeField] private TextMeshProUGUI duration;

        private ObservableBool _reqDone;
        public string InProgressTitle { get; set; }
        public string CommonTitle { get; set; }
        protected override void SetInProgress(bool inProgress)
        {
            base.SetInProgress(inProgress);

            button.Title = inProgress ? InProgressTitle : CommonTitle;
            button.Button.SetInteractivity(!inProgress && _reqDone);
        }

        public void InitializeData(InProgressSingleReqBtnModel model)
        {
            InProgressTitle = model.InProgressTitle;
            CommonTitle = model.CommonTitle;

            _reqDone = model.ReqModel.RequirementsDone;
            
            if (duration != null)
                duration.text = model.Duration;
            
            Initialize(model.InProgress);
        }
    }

    public class InProgressSingleReqBtnModel
    {
        public string InProgressTitle { get; set; }
        public string CommonTitle { get; set; }
        public string Duration { get; set; }
        public SingleRequirementModel ReqModel { get; set; }
        public ObservableBool InProgress { get; set; }
    }
}