using UnityEngine;

namespace UIScripts.Table.DynamicTable
{
    public abstract class DynamicTableColumn<T> : MonoBehaviour
    {
        [SerializeField] protected DynamicTableColumnData<T> data;
        [SerializeField] protected RectTransform rect;

        public DynamicTableColumnData<T> Data
        {
            get => data;
            set
            {
                data = value;
                Initialize();
            }
        }

        protected void Initialize()
        {
            var size = new Vector2(data.Width, rect.sizeDelta.y);
            rect.sizeDelta = size;
        }
    }
}