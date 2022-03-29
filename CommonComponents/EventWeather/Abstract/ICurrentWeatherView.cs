using UIScripts.CommonComponents.WeatherImage;

namespace UIScripts.CommonComponents.EventWeather.Abstract
{
    public interface ICurrentWeatherView : IWeatherView
    {
        public WeatherType WeatherType { get; set; }
        public string Temperature { get; set; }
        public string AsphaltTemperature { get; set; }
        public string Wetness { get; set; }
    }
}