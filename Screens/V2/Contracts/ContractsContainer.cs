using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Contracts
{
    public class ContractsContainer : MonoBehaviour
    {
        [SerializeField] private ContractCategoryContainer work;
        [SerializeField] private ContractCategoryContainer partner;
        [SerializeField] private ContractCategoryContainer client;
        [SerializeField] private TextMeshProUGUI title;
        
        public ContractCategoryContainer Work => work;
        public ContractCategoryContainer Partner => partner;
        public ContractCategoryContainer Client => client;

        public string Title
        {
            set => title.text = value;
        }
    }
}