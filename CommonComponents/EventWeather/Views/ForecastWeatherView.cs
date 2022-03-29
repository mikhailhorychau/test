using TMPro;
using UIScripts.CommonComponents.EventWeather.Abstract;
using UnityEngine;

namespace UIScripts.CommonComponents.EventWeather.Views
{
    public class ForecastWeatherView : WeatherViewBase, IForecastWeatherView
    {
        [SerializeField] private TextMeshProUGUI temperatureTitle;
        [SerializeField] private TextMeshProUGUI rainChanceTitle;

        public string Temperature
        {
            get => temperatureTitle.text;
            set => temperatureTitle.text = value;
        }

        public string RainChance
        {
            get => rainChanceTitle.text;
            set => rainChanceTitle.text = value;
        }
    }
}