using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using ProjectTryout.Model.Services;

namespace ProjectTryout.Model.DataRepresentation
{
    public interface IRepresentation
    {
        void BuildNotification(ActionResult actionResult);
        void BuildView(IAction action);
    }
}
