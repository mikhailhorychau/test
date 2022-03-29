using System;
using UIScripts.Screens.V2.Research.InProgress;
using UIScripts.Screens.V2.Research.Items;
using UnityEngine;

namespace UIScripts.Screens.V2.Research
{
    public class ResearchUI : MonoBehaviour
    {
        [SerializeField] private ResearchInProgress inProgress;
        [SerializeField] private ResearchItemsContainer itemsContainer;

        public ResearchInProgress InProgress => inProgress;
        public ResearchItemsContainer ItemsContainer => itemsContainer;
    }
}