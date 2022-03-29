using System;
using System.Collections.Generic;
using System.Linq;

namespace UIScripts.Arch
{
    public class StorageBase
    {
        private Dictionary<Type, Storage> _storages;
        private SceneConfig _config;

        public StorageBase(SceneConfig config)
        {
            _config = config;
            _storages = new Dictionary<Type, Storage>();
        }

        public void CreateAllStorages()
        {
            _storages = _config.CreateStorages();
        }

        public void InitializeAllStorages() => 
            _storages.Values
                .ToList()
                .ForEach(storage => storage.Initialize());

        public void SendOnCreateToAllStorages() =>
            _storages.Values
                .ToList()
                .ForEach(storage => storage.OnCreate());

        public void SendOnStartToAllStorages() => 
            _storages.Values
                .ToList()
                .ForEach(storage => storage.OnStart());

        public T GetStorage<T>() where T: Storage
        {
            var type = typeof(T);
            var storage = _storages[type];

            return storage as T;
        }
    }
}