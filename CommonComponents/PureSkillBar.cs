using UIScripts.Abstract;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class PureSkillBar : MonoBehaviour, ISkillValue
    {
        private const int MIN = 0;
        private const int MAX = 10;
        
        [SerializeField] private Image fill;
        [SerializeField] private BannedIndicatorsContainer bannedIndicators;

        [Range(MIN, MAX)]
        [SerializeField] private int value;
        
        [Range(MIN, MAX)]
        [SerializeField] private int banned;

        public int Value
        {
            get => value;
            set
            {
                if (bannedIndicators != null)
                {
                    bannedIndicators.Current = value;
                }
                
                if (this.value == value) return;

                this.value = value;
                Resize();
            }
        }

        public int BannedValue
        {
            set
            {
                if (bannedIndicators != null)
                    bannedIndicators.Banned = value;
            }
        }

        public void SetValue(int val) => Value = val;
        public void SetValue(float val) => Value = Mathf.RoundToInt(val);
        private void Resize() => fill.fillAmount = value / (float) MAX;

        private void OnValidate()
        {
            Resize();
            UpdateBanned();
        }

        private void UpdateBanned()
        {
            if (bannedIndicators != null)
            {
                bannedIndicators.Current = value;
                bannedIndicators.Banned = banned;
            }
        }
    }
}