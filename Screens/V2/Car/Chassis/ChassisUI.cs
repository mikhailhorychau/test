using UIScripts.Screens.V2.Car.Chassis.ReadyToProd;
using UIScripts.Screens.V2.Car.Chassis.WorkWithTyres;
using UIScripts.Screens.V2.Car.Efficiency;
using UIScripts.Screens.V2.ResearchProblem;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis
{
    public class ChassisUI : MonoBehaviour
    {
        [SerializeField] private ChassisInfo chassisInfo;
        [SerializeField] private EfficiencyContainer efficiency;
        [SerializeField] private ReadyToProduction readyToProd;
        [SerializeField] private ResearchProblems researchProblems;
        [SerializeField] private ChassisWorkWithTyres workWithTyres;

        public ChassisInfo ChassisInfo => chassisInfo;
        public EfficiencyContainer Efficiency => efficiency;
        public ReadyToProduction ReadyToProd => readyToProd;
        public ResearchProblems ResearchProblems => researchProblems;
        public ChassisWorkWithTyres WorkWithTyres => workWithTyres;
    }
}