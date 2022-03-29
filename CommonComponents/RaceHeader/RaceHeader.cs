using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.CommonComponents
{
    public class RaceHeader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI raceWeekendTitle;
        [SerializeField] private TextMeshProUGUI raceCountryTitle;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image flag;
        [SerializeField] private WeatherCard practiceWeather;
        [SerializeField] private WeatherCard qualificationWeather;
        [SerializeField] private WeatherCard raceWeather;

        public string RaceWeekendTitle
        {
            get => raceWeekendTitle.text;
            set => raceWeekendTitle.text = value;
        }

        public string RaceCountryTitle
        {
            get => raceCountryTitle.text;
            set => raceCountryTitle.text = value;
        }

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public Sprite Flag
        {
            set => flag.overrideSprite = value;
        }

        public WeatherCard PracticeWeather => practiceWeather;
        public WeatherCard QualificationWeather => qualificationWeather;
        public WeatherCard RaceWeather => raceWeather;
    }
}