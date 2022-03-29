using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Tab
{
    public class TabContainer : MonoBehaviour
    {
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private List<Toggle> tabs;
        [SerializeField] private List<GameObject> containers;

        [SerializeField] private int activeTab;

        [SerializeField] private bool toggleCanvasActivity;

        public event Action<int, bool> OnContainerActivityChanged;

        private Toggle _currentToggle;

        public Toggle CurrentToggle => _currentToggle;
        
        public List<Toggle> Tabs
        {
            get => tabs;
            set => tabs = value;
        }

        public List<GameObject> Containers
        {
            get => containers;
            set => containers = value;
        }

        public int ActiveTabID => activeTab;

        private void Start() => Initialize();

        private void Initialize(bool firstSelected = false)
        {
            if (tabs.Count == 0 || containers.Count == 0) return;
            if (firstSelected)
            {
                activeTab = 0;
            }

            tabs.ForEach(tab =>
            {
                tab.onValueChanged.RemoveListener((selected) => ToggleValueChangeListener(tab, selected));
                tab.onValueChanged.AddListener((selected) => ToggleValueChangeListener(tab, selected));
                tab.group = toggleGroup;
            });
            tabs[activeTab].isOn = true;
            UpdateActiveContainer(true);
        }

        public void UpdateTabs(bool firstSelected = false) => Initialize(firstSelected);

        public void UpdateActiveTab(int id)
        {
            activeTab = id;
            tabs[activeTab].isOn = true;
        }

        public void SetTabActivity(int id, bool activity) => tabs[id].gameObject.SetActive(activity);

        // private void UpdateActiveContainer(bool value) => UpdateActiveContainer();

        public void ToggleValueChangeListener(Toggle toggle, bool selected)
        {
            if (selected && toggle == _currentToggle) return;

            _currentToggle = toggle;
            UpdateActiveContainer(selected);
        }
        
        private void UpdateActiveContainer(bool value)
        {
            var toggle = toggleGroup.GetFirstActiveToggle();
            if (!toggle) return;

            if (!value)
            {
                OnContainerActivityChanged?.Invoke(activeTab, false);
                return;
            }

            activeTab = tabs.IndexOf(toggle);
            OnContainerActivityChanged?.Invoke(activeTab, true);
            
            containers.FindAll(container => containers.IndexOf(container) != tabs.IndexOf(toggle))
                .ForEach(container =>
                {
                    if (toggleCanvasActivity)
                    {
                        container.GetComponent<Canvas>().enabled = false;
                    }
                    else
                    {
                        container.SetActive(false);
                    }
                });
            var activeContainer = containers.Find(container => containers.IndexOf(container) == tabs.IndexOf(toggle));
            if (toggleCanvasActivity)
            {
                activeContainer.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                activeContainer.SetActive(true);
            }
        }
    }
}