using UIScripts.Screens.Profile;
using UIScripts.Screens.V2.Development.Factory.Investigate;
using UIScripts.Screens.V2.Development.Factory.ProductionRequest;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Development.Factory
{
    public class FactoryUI : MonoBehaviour
    {
        [SerializeField] private FactoryInvestigate investigate;
        [SerializeField] private ProductionRequestPresenter productionRequests;
        [SerializeField] private ProfileContractList currentContracts;
        [SerializeField] private ProfileContractList nextYearContracts;
        [SerializeField] private Tab.TabButton tab;
        [SerializeField] private Image factoryIcon;
        
        public FactoryInvestigate Investigate => investigate;
        public ProductionRequestPresenter ProductionRequests => productionRequests;
        public ProfileContractList CurrentContracts => currentContracts;
        public ProfileContractList NextYearContracts => nextYearContracts;

        public Tab.TabButton Tab
        {
            get => tab;
            set => tab = value;
        }
        public string Name
        {
            set
            {
                investigate.FactoryName = value;
                tab.SetTextAndResize(value);
            }
        }

        public Sprite FactorySprite
        {
            set => factoryIcon.overrideSprite = value;
        }
    }
}