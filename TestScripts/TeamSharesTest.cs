using System.Collections.Generic;
using UIScripts.Screens.TeamControl.Shares;
using UIScripts.Screens.TeamControl.Shares.Holders;
using UIScripts.Screens.TeamControl.Shares.SharesSell;
using UnityEngine;

namespace UIScripts.TestScripts
{
    public class TeamSharesTest : MonoBehaviour
    {
        [SerializeField] private TeamShares target;

        private SharesModel _model;
        
        private void Awake()
        {
            _model = new SharesModel(GeneratedHolders(), GeneratedSellModel(), default);
            target.Initialize(_model);
            _model.SellModel.SharesCount.OnValueChange += CountChangedListener;
            target.OnSell += SellListener;
        }

        private void SellListener()
        {
            var sellModel = _model.SellModel;
            
            sellModel.MaxShares.Value -= sellModel.SharesCount.Value;
            sellModel.SharesCount.Value = 1;
        }
        
        private void CountChangedListener(int count) => _model.SellModel.TotalCost.Value = $"{count * 500}$";

        public void UpdateHolderRandomPercent(int id) => _model.SharesHolders.Update(id, GenerateHolder(id));

        private List<SharesHolderData> GeneratedHolders()
        {
            var list = new List<SharesHolderData>();
            for (var i = 0; i < 5; i++)
            {
                list.Add(GenerateHolder(i));
            }

            return list;
        }

        private SharesHolderData GenerateHolder(int id) =>
            new SharesHolderData(id, Random.Range(1, 100).ToString(), $"holder [{id}]");

        private SharesSellModel GeneratedSellModel() => 
            new SharesSellModel(1, 25, "default", default);
    }
}