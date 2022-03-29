using System.Collections.Generic;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScripts
{
    public class LvlProgressBar : MonoBehaviour
    {
        [SerializeField] private List<StyledImage> lvlIndicator;
        [SerializeField] private UIColorStyle active;
        [SerializeField] private UIColorStyle learning;

        [SerializeField] private StyledIconButton cancelButton;
        [SerializeField] private StyledIconButton plusButton;

        [SerializeField] private List<Image> researchIndicators;

        [SerializeField] private SingleRequirementButtonPresenter singleReqBtn;
        
        [SerializeField] private int value;
        [SerializeField] private bool isLearning;

        [SerializeField] private bool canLearning;
        
        public bool IsLearning
        {
            get => isLearning;
            set
            {
                isLearning = value;
                UpdateValue();
            }
        }

        public UnityEvent onCancelBtnClick;
        public UnityEvent onPlusButtonClick;
        
        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                UpdateValue();
            }
        }

        public bool CanLearning
        {
            get => canLearning;
            set
            {
                canLearning = value;
                UpdateValue();
            }
        }

        public SingleRequirementButtonPresenter ReqBtn => singleReqBtn;

        private void UpdateValue()
        {
            for (var i = 0; i < lvlIndicator.Count; i++)
            {
                var color = i + 1 <= value ? active : UIColorStyle.Background2;
                lvlIndicator[i].SwapColor(color);
            }
            
            if (isLearning)
                ShowResearchIndicator(value);
            else 
                ClearIndicators();
            
            cancelButton.gameObject.SetActive(isLearning);
            
            var activity = !isLearning & value != 5 & canLearning;
            
            if (singleReqBtn != null)
            {
                singleReqBtn.gameObject.SetActive(activity);
            }
            
            plusButton.gameObject.SetActive(activity);
        }

        private void ShowResearchIndicator(int index)
        {
            if (index > researchIndicators.Count - 1) 
                return;
            
            ClearIndicators();
            researchIndicators[index].SetAlpha(1f);
        }

        private void ClearIndicators() => researchIndicators.ForEach(indicator => indicator.SetAlpha(0f));

        private void Awake()
        {
            cancelButton.onClick.AddListener(onCancelBtnClick.Invoke);
            plusButton.onClick.AddListener(onPlusButtonClick.Invoke);
        }

        private void OnValidate()
        {
            UpdateValue();
        }
    }
}