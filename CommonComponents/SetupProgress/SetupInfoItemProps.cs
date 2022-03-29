using System;
using UnityEngine;

namespace UIScripts.CommonComponents.SetupProgress
{
    [Serializable]
    public class SetupInfoItemProps
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private double sunValue;
        [SerializeField] private double rainValue;

        public SetupInfoItemProps(int id, string title, double sunValue, double rainValue)
        {
            this.id = id;
            this.title = title;
            this.sunValue = sunValue;
            this.rainValue = rainValue;
        }

        public SetupInfoItemProps()
        {
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public double SunValue
        {
            get => sunValue;
            set => sunValue = value;
        }

        public double RainValue
        {
            get => rainValue;
            set => rainValue = value;
        }

        public int ID
        {
            get => id;
            set => id = value;
        }
    }
}