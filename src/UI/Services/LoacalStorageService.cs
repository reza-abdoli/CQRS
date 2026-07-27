using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hanssens.Net;
using UI.Contracts;

namespace UI.Services
{
    public class LoacalStorageService : ILoacalStorageService
    {
        private LocalStorage _localStorage; 
        public LoacalStorageService()
        {
            var config = new LocalStorageConfiguration
            {
                AutoSave = true,
                AutoLoad = true,
                Filename = "HR_APP"
            };
            _localStorage = new LocalStorage(config);
        }
        public void ClearStorageValue(List<string> keys)
        {
            foreach (var key in keys)
            {
                _localStorage.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            return _localStorage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return _localStorage.Get<T>(key);
        }

        public void SetStorageValue<T>(string key, T value)
        {
            _localStorage.Store(key, value);
            _localStorage.Persist();
        }
    }
}