using TMPro;
using UIScripts.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Development.Factory.ProductionRequest
{
    public class RequestItem : UIObjectView<RequestItemData>
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI teamName;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI inProgress;
        [SerializeField] private TextMeshProUGUI duration;
        
        private RequestItemData _data;

        public override RequestItemData GetCurrentData() => _data;

        public override void Initialize(RequestItemData data)
        {
            icon.overrideSprite = data.Sprite;
            teamName.text = data.Name;
            cost.text = data.Cost;
            inProgress.text = data.InProgress;
            duration.text = data.Duration;
        }
    }
}