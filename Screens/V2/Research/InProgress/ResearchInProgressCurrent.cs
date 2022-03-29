using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Research.InProgress
{
    public class ResearchInProgressCurrent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTitle;
        [SerializeField] private TextMeshProUGUI inProgressTitle;
        [SerializeField] private TextMeshProUGUI durationValue;

        public void Initialize(ResearchInProgressCurrentData data)
        {
            var hasData = data != null;
            
            gameObject.SetActive(hasData);
            
            if (!hasData)
            {
                return;
            }

            nameTitle.text = data.Name;
            inProgressTitle.text = data.InProgress;
            durationValue.text = data.Duration;
        }
    }
}