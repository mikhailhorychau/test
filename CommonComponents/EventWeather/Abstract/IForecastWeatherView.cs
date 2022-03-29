namespace UIScripts.CommonComponents.EventWeather.Abstract
{
    public interface IForecastWeatherView : IWeatherView
    {
        public string Temperature { get; set; }
        public string RainChance { get; set; }
    }
}