using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.Screens.V2.Development.Factory.ProductionRequest
{
    public struct RequestItemData : IUIData
    {
        public int ID { get; set; }
        public Sprite Sprite { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string InProgress { get; set; }
        public string Duration { get; set; }
    }

    public enum RequestType
    {
        Repair,
        Upgrade
    }
}