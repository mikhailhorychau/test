using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Table.RecycleTable
{
    public abstract class RecycleTableBase<TBodyProps> : MonoBehaviour, ITablePresenter<TBodyProps>
    {
        [SerializeField] protected RecyclingListView listView;
        [SerializeField] protected ZebraList zebra;

        public List<TBodyProps> BodyProps
        {
            get => _bodyProps;
            set
            {
                if (_bodyProps == value)
                    return;

                var needRefresh = _bodyProps.Count == value.Count;
                
                _bodyProps = value;
                UpdateBodyData();
                
                if (needRefresh)
                    listView.Refresh();
            }
        }

        public void Refresh() => listView.Refresh();

        protected List<TBodyProps> _bodyProps = new List<TBodyProps>();

        private void UpdateBodyData()
        {
            listView.ItemCallback = PopulateItem;

            listView.RowCount = _bodyProps.Count;
            
            OnUpdate();
        }

        protected virtual void PopulateItem(RecyclingListViewItem item, int row)
        {
            
        }

        protected virtual void OnUpdate()
        {
            
        }
    }
}