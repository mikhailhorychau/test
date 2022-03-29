using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils.RecycleStepTable
{
    public class DynamicContainerView : MonoBehaviour
    {
        public DynamicContainerViewItem itemPrefab;
        public float maxVisibleHeight;
        public float itemPadding;
        [SerializeField] private RectTransform _rectTransform;
        
        public delegate void ItemDelegate(DynamicContainerViewItem item, int rowID);
        
        public int RowCount
        {
            get => rowsCount;
            set
            {
                rowsCount = value;
                Refresh();
            }
        }
        

        public ItemDelegate itemCallback;
        private RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();

        public Vector2 ScrollPosition => RectTransform.anchoredPosition;
        
        protected DynamicContainerViewItem[] items;
        protected int visibleItemsCount = 0;
        protected int rowsCount = 1000;
        protected int bufferRowID = 0;
        protected const int RowsAboveBellow = 1;
        
        private bool _isRefreshing;

        public void PreInitializeItems(DynamicContainerViewItem[] preInitItems)
        {
            items = preInitItems;
        }

        public void Refresh()
        {
            _isRefreshing = true;
            RectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, ContentHeight());
            RectTransform.anchoredPosition = Vector2.zero;
            visibleItemsCount = GetVisibleItemsCount();
            RebuildItems(visibleItemsCount);
            bufferRowID = 0;
            _isRefreshing = false;
        }

        public void ContainerPositionChange(Vector2 position)
        {
            RectTransform.anchoredPosition = position;
            OnContainerPositionChange();
        }

        public void OnContainerPositionChange()
        {
            if (_isRefreshing) return;

            var newBufferID = CalculateBufferRowID();

            Rebuild(newBufferID);
        }

        private void Rebuild(int newBufferID)
        {
            if (bufferRowID == newBufferID || newBufferID + visibleItemsCount > rowsCount + 1)
                return;

            if (newBufferID > bufferRowID)
            {
                for (var i = bufferRowID; i < newBufferID; i++)
                {
                    var index = i % visibleItemsCount;
                    var position = GetRowPosition(i + items.Length);
                    items[index].SetPosition(position);

                    var dataIndex = items.Length + i;

                    items[index].name = GetItemName(index, dataIndex);

                    if (dataIndex >= rowsCount)
                    {
                        items[index].gameObject.SetActive(false);
                        continue;
                    }

                    if (!items[index].gameObject.activeSelf)
                        items[index].gameObject.SetActive(true);

                    itemCallback?.Invoke(items[index], dataIndex);
                }
            }
            else
            {
                for (var i = bufferRowID; i >= newBufferID; i--)
                {
                    var index = i % visibleItemsCount;
                    var position = GetRowPosition(i);
                    items[index].SetPosition(position);

                    var dataIndex = i;

                    if (!items[index].gameObject.activeSelf)
                        items[index].gameObject.SetActive(true);

                    items[index].name = GetItemName(index, dataIndex);

                    itemCallback?.Invoke(items[index], dataIndex);
                }
            }

            bufferRowID = newBufferID;
        }

        private void RebuildItems(int count)
        {
            if (items == null)
                items = new DynamicContainerViewItem[0];

            if (count > items.Length)
            {
                var prevCount = items.Length;
                Array.Resize(ref items, count);
                for (var i = prevCount; i < count; i++)
                {
                    items[i] = Instantiate(itemPrefab, RectTransform.transform);
                }
            }

            for (var i = 0; i < items.Length; i++)
            {
                if (i >= rowsCount)
                {
                    items[i].gameObject.SetActive(false);
                    continue;
                }

                if (!items[i].gameObject.activeSelf)
                    items[i].gameObject.SetActive(true);
                var position = GetRowPosition(i);
                items[i].SetPosition(position);
                itemCallback?.Invoke(items[i], i);
            }
        }

        private int CalculateBufferRowID() => CalculateBufferRowID(RectTransform.anchoredPosition);

        private int CalculateBufferRowID(Vector2 pos)
        {
            var calculatedID = (int)(pos.y / RowHeight());
            return calculatedID < 0 ? 0 : calculatedID;
        }

        private int GetVisibleItemsCount() => Mathf.RoundToInt(maxVisibleHeight / RowHeight()) + RowsAboveBellow * 2;
        private float RowHeight() => itemPrefab.Height + itemPadding;
        private float ContentHeight() => RowHeight() * rowsCount - itemPadding;
        private Vector2 GetRowPosition(int index) => new Vector2(0, -index * RowHeight());
        private string GetItemName(int index) => $"[{index}]";

        private string GetItemName(int index, int dataIndex) => $"[{index}] [{dataIndex}]";
    }
}