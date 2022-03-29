using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Table
{
    public abstract class TableBase<TBodyProps> : MonoBehaviour, ITablePresenter<TBodyProps>
    {
        [SerializeField] protected List<TBodyProps> bodyProps;
        [SerializeField] protected GameObject bodyRow;
        [SerializeField] protected Transform bodyContainer;
        [SerializeField] protected ZebraList zebra;

        protected List<GameObject> _rows = new List<GameObject>();
        
        public List<TBodyProps> BodyProps
        {
            get => bodyProps;
            set
            {
                if (value == null) return;
                bodyProps = value;
                InitializeBody();
            }
        }
        
        protected virtual void InitializeBody()
        {
            var imgs = new List<StyledImage>();
            _rows.ForEach(Destroy);
            _rows.Clear();
            
            // foreach (Transform tr in bodyContainer)
            // {
            //     Destroy(tr.gameObject);
            // }
            //
            bodyProps?.ForEach(row =>
            {
                var rowObj = Instantiate(bodyRow, bodyContainer);
                rowObj.GetComponent<IRowPresenter<TBodyProps>>().Initialize(row);
                imgs.Add(rowObj.GetComponent<StyledImage>());
                _rows.Add(rowObj);
                Callback(rowObj);
            });

            if (zebra)
                zebra.ItemsList = imgs;
            
            AfterZebraColorChange();
        }
        
        protected virtual void Callback(GameObject obj) {}
        
        protected virtual void AfterZebraColorChange() {}
    }
}