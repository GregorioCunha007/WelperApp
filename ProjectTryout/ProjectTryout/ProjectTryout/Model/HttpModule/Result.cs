using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTryout.Model.HttpModule
{
    public class Result
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
