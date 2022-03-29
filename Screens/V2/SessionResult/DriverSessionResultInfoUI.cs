using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.SessionResult
{
    public class DriverSessionResultInfoUI : MonoBehaviour
    {
        [SerializeField] private ProfileAvatarClass avatar;
        [SerializeField] private TextMeshProUGUI carTitle;
        [SerializeField] private TextMeshProUGUI driverTitle;

        public void SetAvatar(ProfileAvatarProps avatarSprites) => avatar.Props = avatarSprites;
        public void SetCarTitle(string text) => carTitle.text = text;
        public void SetDriverName(string text) => driverTitle.text = text;
    }
}