using TMPro;
using UIScripts.Screens.Car;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Screens.V2.Car.NewChassis.DevelopBlock
{
    public class NewChassisDevelopUI : MonoBehaviour
    {
        [SerializeField] private CarNewChassisBlock block;
        [SerializeField] private Slider angle;
        [SerializeField] private Slider clearance;
        [SerializeField] private Slider length;
        [SerializeField] private TextMeshProUGUI angleTitle;
        [SerializeField] private TextMeshProUGUI clearanceTitle;
        [SerializeField] private TextMeshProUGUI lengthTitle;

        private NewChassisDevelopModel _developModel;
        
        public CarNewChassisBlock NewChassisBlock => block;
        
        public Slider Angle => angle;
        public Slider Clearance => clearance;
        public Slider Length => length;


        public void Initialize(NewChassisDevelopModel developModel)
        {
            RemoveListeners();
            
            _developModel = developModel;

            angle.value = developModel.Angle.Value;
            clearance.value = developModel.Clearance.Value;
            length.value = developModel.Length.Value;
            
            InitializeStatic();
            
            SetInDevelop(developModel.InDevelop.Value);
            
            AddListeners();
        }

        private void AddListeners()
        {
            if (_developModel != null)
            {
                angle.onValueChanged.AddListener(AngleChangeListener);
                clearance.onValueChanged.AddListener(ClearanceChangeListener);
                length.onValueChanged.AddListener(LengthChangeListener);

                _developModel.InDevelop.OnValueChange += InDevelopChangeListener;
            }
        }

        private void RemoveListeners()
        {
            if (_developModel != null)
            {
                angle.onValueChanged.RemoveListener(AngleChangeListener);
                clearance.onValueChanged.RemoveListener(ClearanceChangeListener);
                length.onValueChanged.RemoveListener(LengthChangeListener);
                
                _developModel.InDevelop.OnValueChange -= InDevelopChangeListener;
            }
        }

        private void SetInDevelop(bool inDevelop)
        {
            angle.gameObject.SetActive(!inDevelop);
            angleTitle.gameObject.SetActive(!inDevelop);
            clearance.gameObject.SetActive(!inDevelop);
            clearanceTitle.gameObject.SetActive(!inDevelop);
            length.gameObject.SetActive(!inDevelop);
            lengthTitle.gameObject.SetActive(!inDevelop);
        }

        private void AngleChangeListener(float value) => _developModel.Angle.Value = value; 
        private void ClearanceChangeListener(float value) => _developModel.Clearance.Value = value; 
        private void LengthChangeListener(float value) => _developModel.Length.Value = value;

        private void InDevelopChangeListener(bool inDevelop) => SetInDevelop(inDevelop);

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void InitializeStatic()
        {
            angleTitle.text = _developModel.Static.Angle;
            clearanceTitle.text = _developModel.Static.Clearance;
            lengthTitle.text = _developModel.Static.Length;
        }
    }
}