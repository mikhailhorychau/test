using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Arch
{
    public abstract class SceneConfig
    {
        public abstract Dictionary<Type, Storage> CreateStorages();
        public abstract Dictionary<Type, Interactor> CreateInteractors();

        public void CreateInteractor<T>(Dictionary<Type, Interactor> interactors) where T : Interactor, new()
        {
            var interactor = new T();
            var type = typeof(T);
    
            interactors[type] = interactor;
        }
        
        public void CreateStorage<T>(Dictionary<Type, Storage> storages) where T : Storage, new()
        {
            var storage = new T();
            var type = typeof(T);

            storages[type] = storage;
        }
    }
}