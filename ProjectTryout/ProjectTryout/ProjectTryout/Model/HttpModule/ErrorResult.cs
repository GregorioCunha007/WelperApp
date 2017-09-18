using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model.HttpModule
{
    public static class ErrorResult
    {
        public static Dictionary<int,ActionResult> ErrorsActionResults = new Dictionary<int, ActionResult>();

        static ErrorResult()
        {
            ErrorsActionResults.Add((int)ErrorResultType.PasswordMismatch,ActionResult.CreateActionUnsuccessfull
                (
                    "Password given doesn't match",
                    true,
                    "404"
                ));
            ErrorsActionResults.Add((int)ErrorResultType.ServerSideError, ActionResult.CreateActionUnsuccessfull
                (
                    "Server unexpected malfunction",
                    true,
                    "500"
                ));
        }
    }
}
