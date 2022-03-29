using System;
using UIScripts.CommonComponents.EventWeather.Abstract;
using UIScripts.CommonComponents.EventWeather.Views;
using UnityEngine;

namespace UIScripts.CommonComponents.EventWeather.Test
{
    public class EventWeatherViewTesterContainer : MonoBehaviour
    {
        [SerializeField] private EventWeatherInitializer eventWeatherInitializer;

        public IEventWeatherInitializer EventWeatherInitializer => eventWeatherInitializer;

        private void Start()
        {
            var test = new EventWeatherViewTest(EventWeatherInitializer);
            test.Test();
        }
    }
}