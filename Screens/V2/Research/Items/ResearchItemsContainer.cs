using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Research.Items
{
    public class ResearchItemsContainer : MonoBehaviour
    {
        public event Action<int> OnStartResearch;
        
        [SerializeField] private ResearchItem prefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private StaticPopup popup;
        [SerializeField] private TextMeshProUGUI stackOfResearchTitle;
        [SerializeField] private ZebraList zebra;

        public StaticPopup Popup => popup;

        public string StackOfResearchTitle
        {
            set => stackOfResearchTitle.text = value;
        }

        public void Initialize(List<ResearchItemData> items)
        {
            var imgs = new List<StyledImage>();
            items.ForEach(researchData =>
            {
                var research = Instantiate(prefab, container);
                research.Initialize(researchData, popup);
                research.OnStartResearch += StartResearchListener;
                
                imgs.Add(research.StyledImage);
            });

            zebra.ItemsList = imgs;
        }

        private void StartResearchListener(int id) => OnStartResearch?.Invoke(id);
    }
}