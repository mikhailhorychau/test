using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Research.Items.Reward
{
    public class SimpleReward : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI value;
        
        public void Initialize(RewardData data)
        {
            description.text = data.Description;
            value.text = data.Value;
        }
    }
}