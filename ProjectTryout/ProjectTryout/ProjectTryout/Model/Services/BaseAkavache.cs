using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;

namespace ProjectTryout.Model.Services
{
    public abstract class BaseAkavache
    {
        protected static Dictionary<string, IBlobCache> AkaCaches;

        public const string LocalMachine = "LocalMachine";
        public const string InMemory = "InMemory";
        public const string Secure = "Secure";
        public const string UserAccount = "UserAccount";

        static BaseAkavache()
        {
            AkaCaches.Add(BaseAkavache.LocalMachine, BlobCache.LocalMachine);
            AkaCaches.Add(BaseAkavache.InMemory, BlobCache.InMemory);
            AkaCaches.Add(BaseAkavache.Secure, BlobCache.Secure);
            AkaCaches.Add(BaseAkavache.UserAccount, BlobCache.UserAccount);
        }
    }
}
