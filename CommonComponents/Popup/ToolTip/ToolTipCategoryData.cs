using System;
using System.Collections.Generic;

namespace UIScripts.CommonComponents.Popup.ToolTip
{
    [Serializable]
    public class ToolTipCategoryData
    {
        public string name;
        public List<string> items;
    }

    [Serializable]
    public class ToolTipSettingsData
    {
        public List<ToolTipCategoryData> categories;
    }
}