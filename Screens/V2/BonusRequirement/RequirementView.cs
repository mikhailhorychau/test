using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.BonusRequirement
{
    [AddComponentMenu("UI/Requirements/Requirement")]
    public class RequirementView : MonoBehaviour, IRequirementView
    {
        public event Action OnViewDestroy;
        
        [SerializeField] private Image icon;
        [SerializeField] private StyledText value;
        
        public Sprite Icon
        {
            set
            {
                if (icon == null) return;
                icon.overrideSprite = value;
            } 
        }

        public string Value
        {
            set => this.value.Text = value;
        }

        public bool CanBeUsed
        {
            set
            {
                var color = value ? UIColorStyle.Text : UIColorStyle.TextRed;
                this.value.SwapColor(color);
            }
        }

        public void SetValue(string v) => Value = v;

        public void SetCanBeUsed(bool canBeUsed) => CanBeUsed = value;
        
        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }
    }
}