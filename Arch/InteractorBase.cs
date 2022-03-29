using System;
using System.Collections.Generic;
using System.Linq;

namespace UIScripts.Arch
{
    public class InteractorBase
    {
        private Dictionary<Type, Interactor> _interactors;
        private SceneConfig _config;

        public InteractorBase(SceneConfig config)
        {
            _config = config;
            _interactors = new Dictionary<Type, Interactor>();
        }

        public void CreateAllInteractors()
        {
            _interactors = _config.CreateInteractors();
        }

        public void InitializeAllInteractors()
        {
            _interactors.Values
                .ToList()
                .ForEach(interactor => interactor.Initialize());
        }
        
        public void SendOnCreateToAllInteractors() =>
            _interactors.Values
                .ToList()
                .ForEach(interactor => interactor.OnCreate());

        public void SendOnStartToAllInteractors() =>
            _interactors.Values
                .ToList()
                .ForEach(interactor => interactor.OnStart());
        
        
        public T GetInteractor<T>() where T: Interactor
        {
            var type = typeof(T);
            var interactor = _interactors[type];

            return interactor as T;
        }
    }
}