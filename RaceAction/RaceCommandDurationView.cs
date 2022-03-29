using TMPro;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public class RaceCommandDurationView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;

        public void SetValue(int value)
        {
            var activity = value != 0;

            gameObject.SetActive(activity);
            var formatValue = GetFormatValue(value);
            title.text = formatValue;
        }

        private string GetFormatValue(int value)
        {
            var minutes = value / 60;
            var seconds = value % 60;

            return $"{minutes}:{seconds:d2}";
        }

    }
}