using UnityEngine;

namespace UIScripts.Screens.V2.Research.Items.Reward
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private SimpleReward simple;
        [SerializeField] private BonusReward bonus;

        public void Initialize(RewardData data)
        {
            var isNullSprite = data.Sprite == null;
            
            if (isNullSprite)
                simple.Initialize(data);
            else
                bonus.Initialize(data);
            
            simple.gameObject.SetActive(isNullSprite);
            bonus.gameObject.SetActive(!isNullSprite);
        }
    }
}