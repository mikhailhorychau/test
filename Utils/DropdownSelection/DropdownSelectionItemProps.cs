using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Utils.DropdownSelection
{
    [Serializable]
    public class DropdownSelectionItemProps
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int selectedID = 0;
        [SerializeField] private bool isPresented;
        [SerializeField] private string warningMessage;
        [SerializeField] private bool withWarning;
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int dropdownStep;
        [SerializeField] private int clickStep;

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

        public int SelectedID
        {
            get => selectedID;
            set => selectedID = value;
        }

        public bool IsPresented
        {
            get => isPresented;
            set => isPresented = value;
        }

        public int MINValue
        {
            get => minValue;
            set => minValue = value;
        }

        public int MAXValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public int DropdownStep
        {
            get => dropdownStep;
            set => dropdownStep = value;
        }

        public int ClickStep
        {
            get => clickStep;
            set => clickStep = value;
        }

        public string WarningMessage
        {
            get => warningMessage;
            set => warningMessage = value;
        }

        public bool WithWarning
        {
            get => withWarning;
            set => withWarning = value;
        }
    }
}