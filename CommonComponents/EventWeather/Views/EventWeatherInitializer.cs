using System;
using System.Collections.Generic;
using System.Linq;
using UIScripts.CommonComponents.EventWeather.Abstract;
using UnityEngine;

namespace UIScripts.CommonComponents.EventWeather.Views
{
    public class EventWeatherInitializer : MonoBehaviour, IEventWeatherInitializer
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private ForecastWeatherView forecastPrefab;
        [SerializeField] private PastStageWeatherView pastStagePrefab;
        [SerializeField] private CurrentWeatherView currentWeatherPrefab;

        private static Type _currentType = typeof(ICurrentWeatherView);
        private static Type _pastStageType = typeof(IPastStageWeatherView);
        private static Type _forecastType = typeof(IForecastWeatherView);

        private int _counter = 0;

        public T InitializeView<T>() where T : IWeatherView => InstantiateView<T>();

        private T InstantiateView<T>() where T : IWeatherView
        {
            var view = GetViewPrefab<T>();

            var obj = Instantiate(view, container);

            if (typeof(T) != _currentType)
            {
                _counter++;
                obj.Background.SwapColor(GetColorByViewIndex(_counter));
            }
            
            return obj.GetComponent<T>();
        }

        private UIColorStyle GetColorByViewIndex(int index) =>
            index % 2 == 0 ? UIColorStyle.Background1 : UIColorStyle.Background2;

        private WeatherViewBase GetViewPrefab<T>() where T : IWeatherView
        {
            var type = typeof(T);

            if (type == _currentType)
                return currentWeatherPrefab;
            if (type == _forecastType)
                return forecastPrefab;

            return pastStagePrefab;
        }
    }
}