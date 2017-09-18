using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;

namespace ProjectTryout.Model.Services
{
    public class AkavacheService<T> : BaseAkavache
    {

        public async void Insert(string key, object obj, string cacheType)
        {
            IBlobCache blob;
            AkaCaches.TryGetValue(cacheType, out blob);
            await blob.InsertObject(key, obj);
        }

        public async Task<bool> IsCached(string key, string cacheType)
        {
            try
            {
                IBlobCache blob;
                AkaCaches.TryGetValue(cacheType, out blob);
                await blob.GetObject<T>(key);
                return true;
            }
            catch (KeyNotFoundException exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        public async Task<T> Get(string key, string cacheType)
        {
            IBlobCache blob;
            AkaCaches.TryGetValue(cacheType, out blob);
            return await blob.GetObject<T>(key);
        }

    }
}
