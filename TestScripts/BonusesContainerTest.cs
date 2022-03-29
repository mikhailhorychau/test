using System;
using System.Collections.Generic;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIScripts.TestScripts
{
    public class BonusesContainerTest : MonoBehaviour
    {
        [SerializeField] private BonusesContainer container;
        [SerializeField] private BonusType bonus;

        private Dictionary<BonusType, BonusModel> _models = new Dictionary<BonusType, BonusModel>();

        private void Awake()
        {
            var count = Enum.GetValues(typeof(BonusType)).Length;
            for (var i = 0; i < count; i++)
            {
                var type = (BonusType) i;
                var value = Random.Range(1, 100);
                var isDecreasing = Random.Range(0, 2) == 1;
                var model = new BonusModel(value.ToString(), isDecreasing, new BonusPopupData());
                
                _models.Add(type, model);
                
                container.GetPresenter(type)?.Initialize(model);
            }
        }

        public void SetRandomValue()
        {
            var value = Random.Range(1, 100);
            var isDecreasing = Random.Range(0, 2) == 1;

            _models[bonus].Value.Value = value.ToString();
            _models[bonus].IsDecreasing.Value = isDecreasing;
        }
    }
}