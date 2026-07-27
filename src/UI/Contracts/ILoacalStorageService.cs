using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Contracts
{
    public interface ILoacalStorageService
    {
        void SetStorageValue<T>(string key, T value);
        void ClearStorageValue(List<string> keys);

        bool Exists(string key);
        T GetStorageValue<T>(string key);

    }
}