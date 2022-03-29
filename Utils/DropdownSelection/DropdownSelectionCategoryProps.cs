using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils.DropdownSelection
{
    [Serializable]
    public class DropdownSelectionCategoryProps
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string placeholderText;
        [SerializeField] private List<DropdownSelectionItemProps> list;

        public int ID
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string PlaceholderText
        {
            get => placeholderText;
            set => placeholderText = value;
        }

        public List<DropdownSelectionItemProps> List
        {
            get => list;
            set => list = value;
        }
    }
}