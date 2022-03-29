using UIScripts.CommonComponents.DropdownButton.ActionButton;
using UIScripts.Screens.CareerProfile;
using UnityEngine;

namespace UIScripts.Screens.V2.StaffProfile
{
    public class StaffProfileUI : MonoBehaviour
    {
        [SerializeField] private StaffProfileInfoClass info;
        [SerializeField] private StaffProfileCareerClass career;
        [SerializeField] private StaffProfileSkills skills;
        [SerializeField] private ActionDropdownButton actionBtn;

        public StaffProfileInfoClass Info => info;
        public StaffProfileCareerClass Career => career;
        public StaffProfileSkills Skills => skills;
        public ActionDropdownButton ActionBtn => actionBtn;
    }
}