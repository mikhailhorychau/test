using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public class ReadyToProductionItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleView;
        [SerializeField] private TextMeshProUGUI nameView;

        private string Decided
        {
            set => titleView.text = value;
        }

        private string Name
        {
            set => nameView.text = value;
        }

        public void Initialize(string nameTitle, string decided)
        {
            Decided = decided;
            Name = nameTitle;
        }
    }
}