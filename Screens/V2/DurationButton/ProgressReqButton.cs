using System;
using TMPro;
using UIScripts.Observable;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.DurationButton
{
    public class ProgressReqButton : MonoBehaviour, IDestroyableView
    {
        public event Action OnViewDestroy;
        
        [SerializeField] private RequirementsButtonPresenter presenter;
        [SerializeField] private GameObject inProgressObj;
        [SerializeField] private TextMeshProUGUI inProgressTitle;
        [SerializeField] private TextMeshProUGUI durationTitle;

        private ProgressReqButtonData _data;
        
        public event Action OnClick
        {
            add => presenter.OnClick += value;
            remove => presenter.OnClick -= value;
        }
        
        public void Initialize(ProgressReqButtonData data)
        {
            _data = data;
            presenter.Initialize(data.buttonData);

            inProgressTitle.text = data.InProgressTitle;
            durationTitle.text = data.DurationValue;

            SetInProgress(data.InProgress);
            
            data.InProgress.AddViewSubscriber(this, SetInProgress);
        }
        
        public void SetInProgress(bool inProgress)
        {
            inProgressObj.SetActive(inProgress);
            presenter.gameObject.SetActive(!inProgress);
        }

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }
    }

    public class ProgressReqButtonData
    {
        public RequirementButtonData buttonData { get; set; }
        public string InProgressTitle { get; set; }
        public string DurationValue { get; set; }
        public ObservableBool InProgress { get; } = new ObservableBool();
    }
}