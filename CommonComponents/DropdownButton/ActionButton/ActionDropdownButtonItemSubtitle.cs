using TMPro;
using UnityEngine;

namespace UIScripts.CommonComponents.DropdownButton.ActionButton
{
    public class ActionDropdownButtonItemSubtitle : ActionDropdownButtonItem
    {
        [SerializeField] private TextMeshProUGUI subtitle;
        public override void Initialize(ActionDropdownButtonData data)
        {
            base.Initialize(data);
            subtitle.text = data.Subtitle;
        }
    }
}