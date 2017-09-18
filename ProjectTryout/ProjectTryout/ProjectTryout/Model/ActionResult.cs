using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model
{
    public class ActionResult : Result
    {
        public bool Succeed { get; set; }
        public bool Notify { get; set; }
        public string Message { get; set; }

        public static ActionResult CreateActionSuccessfull(string message, bool notifyUi)
        {
            return new ActionResult
            {
                Message = message,
                Succeed = true,
                Notify = notifyUi
            };
        }

        public static ActionResult CreateActionUnsuccessfull(string message, bool notifyUi, string statusCode)
        {
            return new ActionResult
            {
                Message = message + ", server error " + statusCode,
                Succeed = false,
                Notify = notifyUi
            };
        }
    }
}
