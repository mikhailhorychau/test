using System;
using UnityEngine;

namespace UIScripts.Table.DynamicTable
{
    [Serializable]
    public class DynamicTableColumnData<T>
    {
        [SerializeField] private float width;
        [SerializeField] private T data;

        public float Width
        {
            get => width;
            set => width = value;
        }

        public T Data
        {
            get => data;
            set => data = value;
        }
    }
}