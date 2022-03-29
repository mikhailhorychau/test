using TMPro;
using UIScripts.CommonComponents.EventWeather.Abstract;
using UIScripts.CommonComponents.WeatherImage;
using UnityEngine;

namespace UIScripts.CommonComponents.EventWeather.Views
{
    public class CurrentWeatherView : WeatherViewBase, ICurrentWeatherView
    {
        [SerializeField] private WeatherImage.WeatherImage weather;
        [SerializeField] private TextMeshProUGUI temperatureTitle;
        [SerializeField] private TextMeshProUGUI asphaltTemperatureTitle;
        [SerializeField] private TextMeshProUGUI wetnessTitle;

        public WeatherType WeatherType
        {
            get => weather.WeatherType;
            set => weather.WeatherType = value;
        }

        public string Temperature
        {
            get => temperatureTitle.text;
            set => temperatureTitle.text = value;
        }

        public string AsphaltTemperature
        {
            get => asphaltTemperatureTitle.text;
            set => asphaltTemperatureTitle.text = value;
        }

        public string Wetness
        {
            get => wetnessTitle.text;
            set => wetnessTitle.text = value;
        }
    }
}