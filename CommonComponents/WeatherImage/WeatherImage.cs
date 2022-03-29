using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents.WeatherImage
{
    public class WeatherImage : MonoBehaviour
    {
        [SerializeField] private Image target;
        [SerializeField] private WeatherType weatherType;
        [SerializeField] private WeatherSprites sprites;

        public WeatherType WeatherType
        {
            get => weatherType;
            set
            {
                if (weatherType == value) 
                    return;
                
                weatherType = value;
                UpdateCurrentSprite(value);
            }
        }

        private Sprite GetSpriteByWeatherType(WeatherType type)
        {
            switch (type) 
            {
                case WeatherType.Sunny : return sprites.sunny;
                case WeatherType.Cloudy : return sprites.cloudy;
                case WeatherType.PartlyCloudy : return sprites.partlyCloudy;
                case WeatherType.Rain : return sprites.rain;
                case WeatherType.HeavyRain : return sprites.heavyRain;
                default : return null;
            };
        }

        private void UpdateCurrentSprite(WeatherType type)
        {
            var sprite = GetSpriteByWeatherType(type);
            target.sprite = sprite;
            target.overrideSprite = sprite;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateCurrentSprite(weatherType);
        }
#endif
    }
}