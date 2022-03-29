using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents.EventWeather.Abstract
{
    public abstract class WeatherViewBase : MonoBehaviour, IWeatherView
    {
        [SerializeField] private TextMeshProUGUI stageTitle;
        [SerializeField] private TextMeshProUGUI dateTitle;
        [SerializeField] private StyledImage background;

        public StyledImage Background => background;

        public string StageType
        {
            get => stageTitle.text;
            set => stageTitle.text = value;
        }

        public string Date
        {
            get => dateTitle.text;
            set => dateTitle.text = value;
        }
    }
}