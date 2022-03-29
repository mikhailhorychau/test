using System;
using System.Collections.Generic;
using UIScripts.CommonComponents.TemperatureRange;
using UIScripts.Observable;
using UIScripts.Screens.Car.TyresTemperatureRange;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Screens.V2.Car.Chassis;
using UIScripts.Screens.V2.Car.Chassis.ReadyToProd;
using UIScripts.Screens.V2.Car.Chassis.WorkWithTyres;
using UIScripts.Screens.V2.Car.Efficiency;
using UIScripts.Screens.V2.ResearchProblem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIScripts.TestScripts.Car
{
    public class CurrentChassisTest : MonoBehaviour
    {
        [SerializeField] private ChassisInfo chassisInfo;
        [SerializeField] private EfficiencyContainer efficiency;
        [SerializeField] private ReadyToProduction readyToProd;
        [SerializeField] private ResearchProblems researchProblems;
        [SerializeField] private ChassisWorkWithTyres workWithTyres;

        private void Awake()
        {
            TestInfo();
            TestEfficiency();
            TestReadyToProd();
            TestResearchProblems();
        }

        private void TestInfo()
        {
            var data = new CurrentChassisInfoData("name", "title", 7, 7, true);
            chassisInfo.Initialize(data);
        }

        private void TestEfficiency()
        {
            var strings = Enum.GetNames(typeof(EfficiencyItemType));
            for (var i = 0; i < strings.Length; i++)
            {
                var data = GeneratedEfficiency(strings[i]);
                efficiency.GetEfficiencyItemByType((EfficiencyItemType) i).Initialize(data);
            }

            efficiency.EfficiencyTitle = "efficiency-title-test";
        }
        
        private EfficiencyModel GeneratedEfficiency(string effName) =>
            new EfficiencyModel(effName, Random.Range(1, 100).ToString());

        private void TestReadyToProd()
        {
            var data = new ReadyToProductionModel()
            {
                DecidedTitle = "decided-test-title",
                MakeImprovementTitle = "make-improvement-test-title",
                NoProblemsTitle = "no-problems-test-title",
                ReadyToToProductionTitle = "ready-to-dev-test-title",
                Problems = GeneratedProblems(),
                DontShowPopup = { Value = false}
            };
            readyToProd.Initialize(data);
            data.DontShowPopup.OnValueChange += (dontShowAgain) => print($"dont-show-again:[{dontShowAgain}]");
            readyToProd.OnStartDevelop += () => print("start-develop");
            readyToProd.OnStartDevelop += () => data.Problems.Value = new List<string>();
        }

        private ObservableProperty<List<string>> GeneratedProblems()
        {
            var list = new List<string>();
            for (var i = 0; i < Random.Range(3, 6); i++)
            {
                list.Add(Utils.Utils.RandomString(10));
            }

            return new ObservableProperty<List<string>>(list);
        }

        private void TestResearchProblems()
        {
            researchProblems.Initialize(GeneratedResearchProblems());
        }

        private List<ResearchProblemModel> GeneratedResearchProblems()
        {
            var list = new List<ResearchProblemModel>();
            for (var i = 0; i < Random.Range(5, 10); i++)
            {
                list.Add(GeneratedResearchProblem(i));
            }
            return list;
        }
        
        private ResearchProblemModel GeneratedResearchProblem(int id)
        {
            var problem = new ResearchProblemModel
            {
                Title = "test-title",
                Description = Utils.Utils.RandomString(Random.Range(20, 200)),
                Duration = "2d",
                ID = id,
                InProgress =
                {
                    Value = false
                },
                RequirementsData = new RequirementButtonData
                {
                    Requirements = GeneratedRequirements(),
                    Title = "solve-test",
                    RequirementsDone =
                    {
                        Value = true
                    }
                }
            };

            return problem;
        }

        private List<RequirementModel> GeneratedRequirements()
        {
            var list = new List<RequirementModel>();
            for (var i = 0; i < Random.Range(1, 3); i++)
            {
                list.Add(GeneratedRequirement(i));
            }

            return list;
        }

        private RequirementModel GeneratedRequirement(int id)
        {
            var requirement = new RequirementModel
            {
                ID = id,
                Icon = null, Value = "100",
                CanBeUsed =
                {
                    Value = true
                }
            };

            return requirement;
        }
    }
}