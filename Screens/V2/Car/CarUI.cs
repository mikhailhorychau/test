using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Car
{
    public class CarUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentChassis;
        [SerializeField] private TextMeshProUGUI nextYearChassis;
        [SerializeField] private TextMeshProUGUI cfd;

        public string CurrentChassisTitle
        {
            set => currentChassis.text = value;
        }

        public string NextYearChassisTitle
        {
            set => nextYearChassis.text = value;
        }

        public string CFDTitle
        {
            set => cfd.text = value;
        }
    }
}