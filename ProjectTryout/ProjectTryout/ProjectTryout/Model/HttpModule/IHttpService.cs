using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectTryout.Model.HttpModule
{
    public interface IHttpService<T>
    {
        Task<T> Get(string uri, object[] uriParameters);
        Task<T> Post(string uri, object[] uriParameters, object data);
    }
}
