using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIScripts.Utils.UI
{
    public class UIObjectsContainer<TData> : UIContainerBehaviour, IObjectContainer<TData> where TData : IUIData
    {
        public event Action OnBecameEmpty;

        [SerializeField] protected UIObjectView<TData> viewPrefab;
        [SerializeField] protected List<UIObjectView<TData>> _preloaded = new List<UIObjectView<TData>>();

        protected Dictionary<int, UIObjectView<TData>> _views = new Dictionary<int, UIObjectView<TData>>();

        public IEnumerable<int> ItemsIDs => _views.Values.Select(view => view.GetCurrentData().ID);

        public override bool IsEmpty()
        {
            return _views.Count == 0;
        }

        public void InitializeItem(TData data)
        {
            var item = GetPreloadedOrInitializedView();
            item.Initialize(data);

            _views.Add(data.ID, item);
            OnInitializeItem(data, item);
            OnContainerSizeChanged(_views.Count);
            RaiseContainerChangedEvent();
        }

        public bool UpdateItem(int id, TData newData)
        {
            if (_views.ContainsKey(id))
            {
                var view = _views[id];
                var prevData = view.GetCurrentData();
                view.Initialize(newData);
                OnUpdateItem(prevData, newData, view);
                OnContainerSizeChanged(_views.Count);
                RaiseContainerChangedEvent();

                return true;
            }

            return false;
        }

        public bool RemoveItem(int id)
        {
            if (_views.ContainsKey(id))
            {
                var view = _views[id];
                // Destroy(view.gameObject);
                view.gameObject.SetActive(false);
                _preloaded.Add(view);
                _views.Remove(id);
                OnRemoveItem(view.GetCurrentData(), view);
                OnContainerSizeChanged(_views.Count);
                RaiseContainerChangedEvent();

                if (IsEmpty())
                    OnBecameEmpty?.Invoke();

                return true;
            }

            return false;
        }

        public void Clear()
        {
            _views.ToList().ForEach(view => RemoveItem(view.Key));
        }

        private UIObjectView<TData> GetPreloadedOrInitializedView()
        {
            if (_preloaded.Count > 0)
            {
                var item = _preloaded.First();
                item.gameObject.SetActive(true);
                _preloaded.Remove(item);
                return item;
            }

            return Instantiate(viewPrefab, container);
        }

        protected virtual void OnInitializeItem(TData data, UIObjectView<TData> view)
        {
        }

        protected virtual void OnUpdateItem(TData prevData, TData newData, UIObjectView<TData> view)
        {
        }

        protected virtual void OnRemoveItem(TData data, UIObjectView<TData> view)
        {
        }

        protected virtual void OnContainerSizeChanged(int count)
        {
        }
    }

    public abstract class UIObjectView<TData> : MonoBehaviour, IDataView<TData> where TData : IUIData
    {
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();

        public abstract TData GetCurrentData();

        public abstract void Initialize(TData data);
    }
}