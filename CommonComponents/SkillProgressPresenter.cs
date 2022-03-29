using UIScripts.Observable;
using UnityEngine;

namespace UIScripts.CommonComponents
{
    public class SkillProgressPresenter : MonoBehaviour
    {
        [SerializeField] private PureSkillBar levelBar;
        [SerializeField] private ProgressIndicators progressBar;

        private SkillProgressModel _model;
        
        private void OnDestroy()
        {
            RemoveListeners();
        }
        
        public void Initialize(SkillProgressModel model)
        {
            RemoveListeners();
            _model = model;
            
            levelBar.Value = model.Level;
            progressBar.Value = model.Progress;
            progressBar.SetType(model.ProgressType);
            
            progressBar.gameObject.SetActive(model.ShowProgress);

            AddListeners();
        }
        
        private void AddListeners()
        {
            _model.Level.OnValueChange += levelBar.SetValue;
            _model.Progress.OnValueChange += progressBar.SetValue;
            _model.ProgressType.OnValueChange += progressBar.SetType;
        }

        private void RemoveListeners()
        {
            if (_model != null)
            {
                _model.Level.OnValueChange -= levelBar.SetValue;
                _model.Progress.OnValueChange -= progressBar.SetValue;
                _model.ProgressType.OnValueChange -= progressBar.SetType;
            }
        }
    }

    public class SkillProgressModel
    {
        public ObservableInt Level { get; } = new ObservableInt();
        public ObservableInt Progress { get; } = new ObservableInt();
        public bool ShowProgress { get; set; }
        public ObservableProperty<ProgressType> ProgressType { get; } = new ObservableProperty<ProgressType>();
        public int BannedLevel { get; set; }

        public SkillProgressModel()
        {
            
        }

        public SkillProgressModel(int level, int progress, bool showProgress, ProgressType type)
        {
            Level = new ObservableInt(level);
            Progress = new ObservableInt(progress);
            ShowProgress = showProgress;
            ProgressType = new ObservableProperty<ProgressType>(type);
        }
        
        
    }
}