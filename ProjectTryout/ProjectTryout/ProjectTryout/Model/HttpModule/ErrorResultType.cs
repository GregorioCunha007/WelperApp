using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTryout.Model.HttpModule
{
    enum ErrorResultType
    {
        PasswordMismatch = 404,
        ServerSideError = 500
    }
}
