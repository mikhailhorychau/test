using UIScripts.Screens.Car.CFD;
using UIScripts.Screens.V2.Car.CFD.Components;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.CFD
{
    public class CFDUI : MonoBehaviour
    {
        [SerializeField] private CarCFDParamsList paramsList;
        [SerializeField] private CarComponentsContainer components;
        [SerializeField] private StaticPopup popup;

        public CarCFDParamsList ParamsList => paramsList;
        public CarComponentsContainer Components => components;
        public StaticPopup ConfirmationPopup => popup;
    }
}