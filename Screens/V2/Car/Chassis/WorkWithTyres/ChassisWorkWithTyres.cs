using System.Collections.Generic;
using TMPro;
using UIScripts.Screens.Car.TyresTemperatureRange;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis.WorkWithTyres
{
    public class ChassisWorkWithTyres : MonoBehaviour
    {
        [SerializeField] private List<CarChassisTemperatureRangeItem> items;
        [SerializeField] private List<CarChassisTemperatureRangeItemProps> props;
        [SerializeField] private TextMeshProUGUI title;

        public string Title
        {
            set => title.text = value;
        }
        
        public void Initialize(List<CarChassisTemperatureRangeItemProps> initProps)
        {
            props = initProps;
            for (var i = 0; i < props.Count; i++)
                items[i]?.Initialize(props[i]);
        }
    }
}