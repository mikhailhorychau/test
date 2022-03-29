using System;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Contracts
{
    public class ContractCategoryContainer : MonoBehaviour
    {
        [SerializeField] private ContractCategoryItem prefab;
        [SerializeField] private Transform container;

        public event Action<TMP_LinkInfo> OnLinkClick; 
        
        public void Initialize(List<Pair<string, bool>> contracts)
        {
            contracts.ForEach(pair =>
            {
                var item = Instantiate(prefab, container);
                item.SetTitle(pair.First);
                item.OnLinkClick += LinkClickListener;
                
                if (pair.Second) 
                    item.SetColor(UIColorStyle.PlayerTableColor);
            });
        }

        private void LinkClickListener(TMP_LinkInfo linkInfo) => OnLinkClick?.Invoke(linkInfo);
    }
}