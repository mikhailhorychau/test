using System;
using TMPro;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Research.Items.Reward;
using UnityEngine;

namespace UIScripts.Screens.V2.Research.Items
{
    public class ResearchItem : MonoBehaviour
    {
        public event Action<int> OnStartResearch;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI nameTitle;
        [SerializeField] private RequirementButton reqBtn;
        [SerializeField] private RewardView reward;
        [SerializeField] private StaticPopup popup;
        [SerializeField] private StyledImage styledImage;

        public StyledImage StyledImage => styledImage;
        
        private ResearchItemData _data;
        private const float DEFAULT_HEIGHT = 66;
        private const float MAX_HEIGHT = 86;

        public void Initialize(ResearchItemData data, StaticPopup staticPopup)
        {
            popup = staticPopup;
            _data = data;
            
            nameTitle.text = data.Name;
            reqBtn.Title = data.Start;
            reqBtn.RequirementsDone = data.ButtonData.RequirementsDone;
            reqBtn.InitializeRequirements(data.ButtonData.Requirements);

            data.ButtonData.RequirementsDone.OnValueChange += ReqDoneListener;

            reqBtn.OnButtonClick += BtnClickListener;
            
            reward.Initialize(data.RewardData);

            if (data.ButtonData.Requirements.Count > 2)
            {
                var size = rectTransform.sizeDelta;
                rectTransform.sizeDelta = new Vector2(size.x, MAX_HEIGHT);
            }
        }

        private void OnDestroy()
        {
            reqBtn.OnButtonClick -= BtnClickListener;

            if (_data == null) return;

            _data.ButtonData.RequirementsDone.OnValueChange -= ReqDoneListener;
        }

        private void ReqDoneListener(bool isDone)
        {
            reqBtn.RequirementsDone = isDone;
        }
        private void BtnClickListener()
        {
            if (_data == null) return;
            
            popup.gameObject.SetActive(true);
            popup.Title = _data.ConfirmationTitle;
            popup.onAccept.AddListener(() => OnStartResearch?.Invoke(_data.ID));
        }
    }
}