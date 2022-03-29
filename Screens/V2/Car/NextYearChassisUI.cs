using UIScripts.Screens.V2.Car.Chassis;
using UIScripts.Screens.V2.Car.NewChassis;
using UnityEngine;

namespace UIScripts.Screens.V2.Car
{
    public class NextYearChassisUI : MonoBehaviour
    {
        [SerializeField] private NewChassisUI newChassis;
        [SerializeField] private ChassisUI chassis;

        public NewChassisUI NewChassis => newChassis;
        public ChassisUI Chassis => chassis;
    }
}