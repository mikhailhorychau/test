using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace UIScripts.TitleValue
{
    public class PairViewContainer<TTitle, TValue> : MonoBehaviour
    {
        [SerializeField] private TitleStringValue prefab;
        [SerializeField] private RectTransform container;

        public void Initialize(List<Pair<TTitle, TValue>> props)
        {
            props.ForEach(itemProps =>
            {
                var itemView = Instantiate(prefab, container);
                itemView.Title = itemProps.First.ToString();
                itemView.Value = itemProps.Second.ToString();
            });
        }
    }
}