using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils.RecycleStepTable.Test
{
    public class ContainerTest : MonoBehaviour
    {
        public DynamicContainerView viewContainer;

        private List<string> _data = new List<string>();

        public void Init()
        {
            var dataSize = Random.Range(1, 100);
            _data = new List<string>();

            for (var i = 0; i < dataSize; i++)
            {
                _data.Add(i.ToString());
            }
            
            viewContainer.itemCallback = ItemCallback;
            viewContainer.RowCount = dataSize;
        }
        
        private void ItemCallback(DynamicContainerViewItem item, int rowID)
        {
            var row = item as ContainerTestItem;
            row.Text = _data[rowID];
        }
    }
}