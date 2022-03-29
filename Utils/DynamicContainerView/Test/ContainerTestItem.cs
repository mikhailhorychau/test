using TMPro;
using UnityEngine;

namespace UIScripts.Utils.RecycleStepTable.Test
{
    public class ContainerTestItem : DynamicContainerViewItem
    {
        [SerializeField] private TextMeshProUGUI title;

        public string Text
        {
            set => title.text = value;
        }
    }
}