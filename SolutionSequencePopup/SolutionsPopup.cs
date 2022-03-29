using System.Collections.Generic;
using UIScripts.Screens.Panels.Popups.ImportantEvent;
using UIScripts.Screens.Panels.Popups.TeamEvent;
using UnityEngine;
using UnityEngine.UI;
using TabBtn = UIScripts.Tab.TabButton;

namespace UIScripts.SolutionSequencePopup
{
    public class SolutionsPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private TabBtn tabPrefab;
        [SerializeField] private RectTransform tabContainer;
        [SerializeField] private ToggleGroup group;

        private List<TabBtn> _tabs = new List<TabBtn>();

        public int Count { get; private set; }

        public void Mount<T>(T decisionComponent) where T : MonoBehaviour, IDecisionComponent
        {
            Count++;
            var obj = decisionComponent.gameObject;
            obj.SetActive(false);
            obj.transform.SetParent(target);
            
            var tab = Instantiate(tabPrefab, tabContainer);
            _tabs.Add(tab);
            
            var toggle = tab.Toggle;
            toggle.group = group;

            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;
            toggle.onValueChanged.AddListener((selected) =>
            {
                obj.SetActive(selected);
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            });

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

            decisionComponent.OnMakeDecision += MakeDecisionListener;
            decisionComponent.OnMakeDecision += () =>
            {
                _tabs.Remove(tab);
                Destroy(tab.gameObject);
                Destroy(decisionComponent.gameObject);
                UpdateTabNumbers();
            };

            _tabs[0].Toggle.isOn = true;
            UpdateTabNumbers();
        }

        private void MakeDecisionListener()
        {
            Count--;
            if (Count == 0)
                gameObject.SetActive(false);
        }

        private void UpdateTabNumbers()
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                var number = i + 1;
                var text = number.ToString();
                _tabs[i].SetTextAndResize(text);
            }
        }
    }

    public class Test
    {
        public SolutionsPopup Popup;
        public TeamEventPopup EventPopup;
        public ImportantEventPopup ImportantEventPopup;
        public StaticPopup StaticPopup;
        
        public void Main()
        {
            Popup.Mount(EventPopup);
        }
    }
}