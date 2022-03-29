using TMPro;
using UnityEngine;

namespace UIScripts.Screens.V2.Research.InProgress
{
    public class ResearchInProgress : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI chooseParamTitle;
        [SerializeField] private TextMeshProUGUI inProgressTitle;
        [SerializeField] private ResearchInProgressCurrent current;

        private ResearchInProgressData _data;

        public string InProgressTitle
        {
            set => inProgressTitle.text = value;
        }
        
        public void Initialize(ResearchInProgressData data)
        {
            _data = data;
            current.Initialize(data.Current);

            chooseParamTitle.text = data.ChooseParamForWorking;

            data.Current.OnValueChange += OnCurrentDataChange;
        }

        private void OnDestroy()
        {
            if (_data != null)
            {
                _data.Current.OnValueChange -= OnCurrentDataChange;
            }
        }

        private void OnCurrentDataChange(ResearchInProgressCurrentData data)
        {
            current.Initialize(data);   
        }
    }
}