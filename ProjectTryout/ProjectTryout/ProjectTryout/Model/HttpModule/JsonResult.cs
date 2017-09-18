using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model.HttpModule
{
    public class JsonResult : Result
    {
        public UserCards Data { get; }

        public JsonResult(string content)
        {
            Data = JsonConvert.DeserializeObject<UserCards>(content);
        }
    }
}
