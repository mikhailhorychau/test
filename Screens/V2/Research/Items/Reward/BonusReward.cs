using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Research.Items.Reward
{
    public class BonusReward : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI value;

        public void Initialize(RewardData data)
        {
            icon.overrideSprite = data.Sprite;
            value.text = data.Value;
        }
    }
}