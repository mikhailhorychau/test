using UIScripts.CommonComponents.EventWeather.Abstract;
using UIScripts.CommonComponents.WeatherImage;

namespace UIScripts.CommonComponents.EventWeather.Test
{
    public class EventWeatherViewTest
    {
        private readonly IEventWeatherInitializer _initializer;

        public EventWeatherViewTest(IEventWeatherInitializer initializer)
        {
            _initializer = initializer;
        }

        public void Test()
        {
            TestPastEvent();
            TestPastEvent();
            TestCurrentEvent();
            TestPastEvent();
            TestForecastEvent();
        }

        private void TestPastEvent()
        {
            var pastEventView = _initializer.InitializeView<IPastStageWeatherView>();

            pastEventView.Date = "date";
            pastEventView.StageType = "past-event-stage";
        }

        private void TestCurrentEvent()
        {
            var currentEventView = _initializer.InitializeView<ICurrentWeatherView>();

            currentEventView.Date = "date";
            currentEventView.Temperature = "temp";
            currentEventView.Wetness = "wetness";
            currentEventView.AsphaltTemperature = "asph-temp";
            currentEventView.WeatherType = WeatherType.HeavyRain;
            currentEventView.StageType = "current-event-stage";
        }

        private void TestForecastEvent()
        {
            var forecastEventView = _initializer.InitializeView<IForecastWeatherView>();

            forecastEventView.Temperature = "temp";
            forecastEventView.RainChance = "rain-chance";
            forecastEventView.Date = "date";
            forecastEventView.StageType = "forecast-event-stage";
        }
    }
}