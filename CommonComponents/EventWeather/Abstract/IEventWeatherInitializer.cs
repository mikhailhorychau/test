namespace UIScripts.CommonComponents.EventWeather.Abstract
{
    public interface IEventWeatherInitializer
    {
        public T InitializeView<T>() where T : IWeatherView;
    }
}