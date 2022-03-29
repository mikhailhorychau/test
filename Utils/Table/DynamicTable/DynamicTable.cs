using System;
using System.Collections.Generic;
using System.Linq;
using UIScripts.Screens.Search;
using UnityEngine;

namespace UIScripts.Table.DynamicTable
{
    public abstract class DynamicTable<T> : TableBase<T>
    {
        [Serializable]
        private class StaticColumn
        {
            public string text;
            public float scale;
        } 
    
        
        [SerializeField] private GameObject headerColumnPrefab;
        [SerializeField] private Transform headerContainer;
        [SerializeField] private List<StaticColumn> staticColumns;
        [SerializeField] private float dynamicColumnSize;
        [SerializeField] private List<string> dynamicColumns;

        private RectTransform _headerRect;
        private List<DynamicTableHeaderColumn> _sortColumns = new List<DynamicTableHeaderColumn>();

        private void Initialize()
        {
            if (!_headerRect) _headerRect = headerContainer.GetComponent<RectTransform>();
            
            foreach (Transform tr in headerContainer)
            {
                Destroy(tr.gameObject);
            }
            
            _sortColumns.Clear();

            var staticWidth = _headerRect.sizeDelta.x - dynamicColumns.Count * dynamicColumnSize;

            var index = 0;
            staticColumns.ForEach(col => 
                InitializeHeaderColumn(col.text, col.scale * staticWidth, false, index++));
            
            dynamicColumns.ForEach(col => 
                InitializeHeaderColumn(col, dynamicColumnSize, dynamicColumns.Last() == col, index++));
            _sortColumns[0].SwitchState(SearchHeaderState.TopToBottom);
            // InitializeBody();
        }

        private void InitializeHeaderColumn(string text, float width, bool isLast = false, int id = -1)
        {
            var obj = Instantiate(headerColumnPrefab, headerContainer);
            var colClass = obj.GetComponent<DynamicTableHeaderColumn>();
            colClass.Initialize(text, width, isLast, id);
            colClass.onSelect.AddListener(OnSortSelect);
            _sortColumns.Add(colClass);
        }

        private void OnSortSelect(int id)
        {
            _sortColumns.FindAll(col => col.ID != id)
                .ForEach(el => el.SwitchState(SearchHeaderState.Common));
        }

        private void StringSort(int id)
        {
            
        }

        private void Start() => Initialize();
    }
}