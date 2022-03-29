using System.Collections.Generic;

namespace UIScripts.Screens.V2.Car.Chassis.ReadyToProd
{
    public class ReadyToProductionPresenter
    {
        private ReadyToProductionModel _model;
        private readonly IReadyToProductionView _view;
        public ReadyToProductionPresenter(ReadyToProductionModel model, IReadyToProductionView view)
        {
            _model = model;
            _view = view;

            Initialize(model);
        }

        private void Initialize(ReadyToProductionModel model)
        {
            _view.InitializeProblems(model.Problems.Value, model.DecidedTitle);

            InitializeStatic();
            
            AddListeners();
        }

        private void AddListeners()
        {
            if (_model != null)
            {
                _model.Problems.OnValueChange += ProblemsChangedListener;
            }

            if (_view != null)
            {
                _view.OnViewDestroy += RemoveListeners;
                _view.OnDontShowAgainChanged += DontShowAgainChangedListener;
            }
        }

        private void RemoveListeners()
        {
            if (_model != null)
            {
                _model.Problems.OnValueChange -= ProblemsChangedListener;
            }
            
            if (_view != null)
            {
                _view.OnViewDestroy -= RemoveListeners;
                _view.OnDontShowAgainChanged -= DontShowAgainChangedListener;
            }
        }

        private void DontShowAgainChangedListener(bool dontShowAgain)
        {
            if (_model != null)
                _model.DontShowPopup.Value = dontShowAgain;
        }

        private void ProblemsChangedListener(List<string> problems)
        {
            _view.InitializeProblems(problems, _model.DecidedTitle);
        }

        private void InitializeStatic()
        {
            _view.NoProblemsTitle = _model.NoProblemsTitle;
            _view.ReadyToProductionTitle = _model.ReadyToToProductionTitle;
            _view.MakeImprovementTitle = _model.MakeImprovementTitle;
        }
    }
}