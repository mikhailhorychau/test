using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public class ReadyToProductionView : MonoBehaviour, IReadyToProductionView
    {
        public event Action<bool> OnDontShowAgainChanged;
        public event Action OnViewDestroy;

        [SerializeField] private TextMeshProUGUI titleView;
        [SerializeField] private TextMeshProUGUI noProblemsView;
        [SerializeField] private ReadyToProductionItem itemPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private StyledButton button;
        [SerializeField] private InfoPopup popup;
        [SerializeField] private StaticPopup confirmationPopup;
        [SerializeField] private bool isNextYearChassis;

        private List<GameObject> _objects = new List<GameObject>();
        private bool _dontShowAgain;
        
        public event Action OnStartDevelop
        {
            add => button.onClick.AddListener(value.Invoke);
            remove => button.onClick.RemoveListener(value.Invoke);
        }

        public InfoPopup Popup => popup;
        public StaticPopup ConfirmationPopup => confirmationPopup;
        
        public string ReadyToProductionTitle
        {
            set => titleView.text = value;
        }

        public string NoProblemsTitle
        {
            set => noProblemsView.text = value;
        }

        public string MakeImprovementTitle
        {
            set => button.Text = value;
        }
        
        private void Awake()
        {
            popup.onToggleValueChange.AddListener(ToggleValueChangedListener);
            button.OnClick.AddListener(BtnClickListener);
        }

        private void OnDestroy()
        {
            popup.onToggleValueChange.RemoveListener(ToggleValueChangedListener);
            button.OnClick.RemoveListener(BtnClickListener);
            
            OnViewDestroy?.Invoke();
        }

        public void InitializeProblems(List<string> problems, string decided, bool dontShowAgain = false)
        {
            _objects.ForEach(Destroy);
            _objects.Clear();

            _dontShowAgain = dontShowAgain;
            
            problems.ForEach(problem => InitializeProblem(problem, decided));

            var hasNoProblems = problems.Count == 0;
            
            SetNoProblemsActivity(hasNoProblems);
        }

        private void InitializeProblem(string problem, string decided)
        {
            var item = Instantiate(itemPrefab, container);
            item.Initialize(problem, decided);
            _objects.Add(item.gameObject);
        }

        private void SetNoProblemsActivity(bool hasNoProblems)
        {
            noProblemsView.gameObject.SetActive(hasNoProblems);
            button.Disabled = hasNoProblems;
        }

        private void BtnClickListener()
        {
            if (isNextYearChassis)
            {
                confirmationPopup.gameObject.SetActive(true);
                if (!_dontShowAgain)
                {
                    confirmationPopup.onAccept.AddListener(() => popup.gameObject.SetActive(true));
                }
                return;
            }
            if (_dontShowAgain) return;
            popup.gameObject.SetActive(true);
        } 
            
        private void ToggleValueChangedListener(bool dontShowAgain) => OnDontShowAgainChanged?.Invoke(dontShowAgain);
        
    }
}