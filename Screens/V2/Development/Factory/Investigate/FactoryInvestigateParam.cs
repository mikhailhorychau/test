using System;
using System.Collections.Generic;
using TMPro;
using UIScripts.Observable;
using UIScripts.Screens.V2.DurationButton;
using UIScripts.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Development.Factory.Investigate
{
    public class FactoryInvestigateParam : MonoBehaviour, IDestroyableView
    {
        public event Action OnStartInvestigate;
        public event Action OnViewDestroy;

        [SerializeField] private TextMeshProUGUI paramName;
        [SerializeField] private SkillBar skillBar;
        [SerializeField] private Sprite investigateSprite;
        [SerializeField] private Sprite investigateBannedSprite;
        [SerializeField] private List<Image> investigatedIndicators;
        [SerializeField] private ProgressReqButton progressButton;
        [SerializeField] private StaticPopup popup;

        private string _confirmationTitle;

        public void Initialize(FactoryInvestigateParamData data)
        {
            paramName.text = data.Name;
            skillBar.BannedValue = data.BannedLevel;

            progressButton.OnClick += BtnClickListener;
            progressButton.Initialize(data.ProgressData);
            SetLevel(data.Level);
            
            var isMaxLevel = data.Level == App.runtime.MaxLevel;
            progressButton.gameObject.SetActive(!isMaxLevel && !data.ProgressData.InProgress);

            _confirmationTitle = data.ConfirmationTitle;

            SetInInvestigate(data.InInvestigate);
            data.InInvestigate.AddViewSubscriber(this, SetInInvestigate);
            data.Level.AddViewSubscriber(this, SetLevel);
        }

        private void SetLevel(int level)
        {
            skillBar.Value = level;
        }

        private void SetInInvestigate(bool value)
        {
            if (value)
                ShowInvestigate();
            else
                HideInvestigate();
        }

        private void ShowIndicator(int lvl)
        {
            for (var i = 0; i < investigatedIndicators.Count; i++)
            {
                var indicator = investigatedIndicators[i];
                var isCurrentLvl = i == lvl;
                
                if (isCurrentLvl)
                {
                    var isBanned = i >= skillBar.BannedValue - 1 && skillBar.BannedValue != 0;
                    var sprite = isBanned ? investigateBannedSprite : investigateSprite;
                    indicator.overrideSprite = sprite;
                }
                
                indicator.enabled = isCurrentLvl;
            }
        }

        private void ShowInvestigate() => ShowIndicator(skillBar.Value);
        private void HideInvestigate() => investigatedIndicators.ForEach(indicator => indicator.enabled = false);

        private void BtnClickListener()
        {
            popup.gameObject.SetActive(true);
            popup.Title = _confirmationTitle;
            popup.onAccept.AddListener(PopupAcceptListener);
        }

        private void PopupAcceptListener()
        {
            OnStartInvestigate?.Invoke();
        }

        private void OnDestroy()
        {
            progressButton.OnClick -= BtnClickListener;
            OnViewDestroy?.Invoke();
        }
    }
}