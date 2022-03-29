using UIScripts.Screens.Panels.Popups.ImportantEvent;
using UIScripts.Screens.Panels.Popups.TeamEvent;
using UnityEngine;

namespace UIScripts.SolutionSequencePopup
{
    public class SolutionsPopupTest : MonoBehaviour
    {
        [SerializeField] private SolutionsPopup popup;
        [SerializeField] private TeamEventPopup evtPopup;
        [SerializeField] private ImportantEventPopup importantPopup;
        

        private void Start()
        {
            // popup.Mount(evtPopup);
            // popup.Mount(importantPopup);
        }
    }
}