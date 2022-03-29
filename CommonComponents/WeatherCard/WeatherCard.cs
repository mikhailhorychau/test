using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class WeatherCard : MonoBehaviour
    {
        [SerializeField] private StyledText typeTitle;
        [SerializeField] private StyledText dateTitle;
        [SerializeField] private StyledText temperatureTitle;
        [SerializeField] private StyledText rainChanceTitle;
        [SerializeField] private StyledImage temperatureIcon;
        [SerializeField] private StyledImage rainIcon;

        [SerializeField] private bool isPastEvent;

        public string Type
        {
            get => typeTitle.Text;
            set => typeTitle.Text = value;
        }
        
        public string Temperature
        {
            get => temperatureTitle.Text;
            set => temperatureTitle.Text = value;
        }

        public string RainChance
        {
            get => rainChanceTitle.Text;
            set => rainChanceTitle.Text = value;
        }

        public string Date
        {
            get => dateTitle.Text;
            set => dateTitle.Text = value;
        }

        public bool IsPastEvent
        {
            get => isPastEvent;
            set
            {
                isPastEvent = value;
                UpdateState();
            }
        }

        private void UpdateState()
        {
            var alpha = isPastEvent ? 0.3f : 1f;
            
            typeTitle?.SetAlpha(alpha);
            dateTitle?.SetAlpha(alpha);
            temperatureTitle?.SetAlpha(alpha);
            rainChanceTitle?.SetAlpha(alpha);
            
            temperatureIcon?.SetAlpha(alpha);
            rainIcon?.SetAlpha(alpha);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateState();
        }
#endif
    } 
}