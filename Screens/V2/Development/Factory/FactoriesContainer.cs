using System.Collections.Generic;
using UIScripts.Tab;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.Factory
{
    public class FactoriesContainer : MonoBehaviour
    {
        [SerializeField] private List<FactoryUI> factories;
        [SerializeField] private List<Tab.TabButton> tabs;
        [SerializeField] private TabContainer tabContainer;

        private Dictionary<int, FactoryUI> _uiFactories = new Dictionary<int, FactoryUI>();
        
        public void Initialize(List<int> indexes)
        {
            for (var i = 0; i < indexes.Count; i++)
            {
                var factory = GetFactoryObject();
                var tab = GetTabObject();

                factory.Tab = tab;
                
                tabContainer.Containers.Add(factory.gameObject);
                tabContainer.Tabs.Add(tab.Toggle);
                
                _uiFactories.Add(indexes[i], factory);
            }
            
            factories.ForEach(factory => factory.gameObject.SetActive(false));
            tabs.ForEach(tab => tab.gameObject.SetActive(false));
        }

        public FactoryUI GetFactory(int index) => _uiFactories[index];

        private FactoryUI GetFactoryObject()
        {
            var obj = factories[0];
            factories.RemoveAt(0);

            return obj;
        }

        private Tab.TabButton GetTabObject()
        {
            var obj = tabs[0];
            tabs.RemoveAt(0);

            return obj;
        }
    }
}