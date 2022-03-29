using System;
using System.Collections;
using UnityEngine;

namespace UIScripts.Arch
{
    public class Scene
    {
        private StorageBase _storageBase;
        private InteractorBase _interactorBase;
        private SceneConfig _config;

        public event Action OnComponentInitialized;
        public event Action OnSceneInitialized;

        public Scene(SceneConfig config)
        {
            _config = config;
            _storageBase = new StorageBase(config);
            _interactorBase = new InteractorBase(config);
        }

        public IEnumerator Initialize()
        {
            _storageBase.CreateAllStorages();
            _interactorBase.CreateAllInteractors();
            yield return new WaitForEndOfFrame();
            
            _storageBase.SendOnCreateToAllStorages();
            _interactorBase.SendOnCreateToAllInteractors();
            yield return new WaitForEndOfFrame();
            
            _storageBase.InitializeAllStorages();
            _interactorBase.InitializeAllInteractors();
            yield return new WaitForEndOfFrame();
            
            OnComponentInitialized?.Invoke();
            
            _storageBase.SendOnStartToAllStorages();
            _interactorBase.SendOnStartToAllInteractors();
            yield return new WaitForEndOfFrame();
            
            OnSceneInitialized?.Invoke();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return _interactorBase.GetInteractor<T>();
        }

        public T GetStorage<T>() where T : Storage
        {
            return _storageBase.GetStorage<T>();
        }
    }
}