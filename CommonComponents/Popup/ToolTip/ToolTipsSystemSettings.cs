using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip
{
    [CreateAssetMenu(fileName = "ToolTipsSystemSettings", menuName = "UI/ToolTipsSystem/Settings", order = 0)]
    
    public class ToolTipsSystemSettings : ScriptableObject
    {
        [SerializeField] private ToolTipSettingsData data;
    }
}