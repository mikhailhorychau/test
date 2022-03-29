using System;
using UIScripts.Screens.Panels.Popups.TeamEvent;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UIScripts.TestScripts
{
    public class TeamEventTest : MonoBehaviour
    {
        [SerializeField] private TeamEventPopup popup;
        [SerializeField] private Button showBtn;
        
        private void Awake()
        {
            // popup.OnFirstActionClick += FirstActionClickListener;
            // popup.OnSecondActionClick += SecondActionClickListener;
            
            showBtn.onClick.AddListener(Show);
            
            popup.Initialize(GeneratedEventData());
        }

        private TeamEventPopupData GeneratedEventData() =>
            new TeamEventPopupData()
            {
                Title = Utils.Utils.RandomString(Random.Range(20, 100)),
                Description = Utils.Utils.RandomString(Random.Range(100, 200)),
                // FirstActionDescription = Utils.Utils.RandomString(Random.Range(25, 120)),
                // SecondActionDescription = Utils.Utils.RandomString(Random.Range(25, 120)),
                // FirstActionTitle = "First action",
                // SecondActionTitle = "Second action"
            };

        private void Show()
        {
            popup.Show();
        }

        private void FirstActionClickListener()
        {
            popup.Hide();
            print("First action");
        }

        private void SecondActionClickListener()
        {
            popup.Hide();
            print("Second action");
        }
    }
}