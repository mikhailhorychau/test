using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.ResearchProblem
{
    public class ResearchProblems : MonoBehaviour
    {
        [SerializeField] private ResearchProblemView prefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private ZebraList zebra;
        [SerializeField] private TextMeshProUGUI stackOfResearch;

        public string StackOfResearch
        {
            set => stackOfResearch.text = value;
        }
        
        private ResearchProblemsPresenter _presenter = new ResearchProblemsPresenter();

        public void Initialize(List<ResearchProblemModel> problems)
        {
            var backgrounds = new List<StyledImage>();
            problems.ForEach(problem =>
            {
                var view = Instantiate(prefab, container);
                _presenter.InitializeProblem(view, problem);
                
                backgrounds.Add(view.Background);
            });

            zebra.ItemsList = backgrounds;
        }
    }
}