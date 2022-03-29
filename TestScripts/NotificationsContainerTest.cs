using System;
using UIScripts.Screens.Panels.HeaderMenu.Notifications;
using UIScripts.Screens.Panels.Popups;
using UnityEngine;

namespace UIScripts.TestScripts
{
    public class NotificationsContainerTest : MonoBehaviour
    {
        [SerializeField] private NotificationsContainer container;
        [SerializeField] private NotificationSpawner spawner;
        [SerializeField] private NewsPopup popup;

        private int _index = 0;
        
        private void Awake()
        {
            container.OnNotificationSelect += NotificationSelectListener;
        }

        public void AddNotification()
        {
            var notification = new Notification() {ID = ++_index, Description = $"{_index} Description"};
            spawner.AddToSpawnQueue(notification);
        }

        private void NotificationSelectListener(int id)
        {
            print(id);
            if (id == 1)
            {
                popup.Show();
            }
        }
    }
}