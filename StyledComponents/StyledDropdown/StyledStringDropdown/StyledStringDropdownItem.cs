using TMPro;
using UnityEngine;

namespace UIScripts
{
    [RequireComponent(typeof(ObjectPointerController))]
    public class StyledStringDropdownItem : StyledDropdownItem2<StringProps>
    {
        [SerializeField] private TextMeshProUGUI text;
        public override void Initialize(StringProps props)
        {
            base.Initialize(props);
            text.text = props.value;
        }
    }
}