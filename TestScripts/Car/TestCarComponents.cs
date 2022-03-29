using System;
using System.Collections.Generic;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Car.CFD.Components;
using UnityEngine;

namespace UIScripts.TestScripts.Car
{
    public class TestCarComponents : MonoBehaviour
    {
        [SerializeField] private CarComponentsContainer target;

        private void Awake()
        {
            target.Initialize(new Dictionary<CarComponentType, CarComponentModel>()
            {
                {CarComponentType.CoolingSystem, new CarComponentModel()
                {
                    Name = "cooling",
                    Level = 9,
                    Duration = "33w",
                    InProgress = { Value = false},
                    RequirementsData = new RequirementButtonData()
                    {
                        Requirements = new List<RequirementModel>() {new RequirementModel()
                        {
                            ID = 0,
                            Icon = null,
                            Value = "100",
                            CanBeUsed = { Value = true},
                        }},
                        RequirementsDone = { Value = true}
                    }
                }}
            });
        }
    }
}