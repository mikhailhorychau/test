using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.Factory.Investigate
{
    public class FactoryInvestigate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI factoryName;
        [SerializeField] private FactoryInvestigateParam power;
        [SerializeField] private FactoryInvestigateParam reliability;
        [SerializeField] private GameObject bannedObj;
        [SerializeField] private TextMeshProUGUI bannedTitle;

        public string FactoryName
        {
            set => factoryName.text = value;
        }
        
        public FactoryInvestigateParam Power => power;
        public FactoryInvestigateParam Reliability => reliability;
        public GameObject BannedObject => bannedObj;

        public string BannedTitle
        {
            set => bannedTitle.text = value;
        }
    }
}