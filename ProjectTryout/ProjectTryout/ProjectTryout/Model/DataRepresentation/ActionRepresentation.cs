using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTryout.Model.Action_Types;
using ProjectTryout.Views;
using Xamarin.Forms;

namespace ProjectTryout.Model.DataRepresentation
{
    public class ActionRepresentation
    {

        public static Dictionary<int, Delegate> PageGivenAction;

        public delegate ContentPage PageFactoryDelegate(string name);

        public static void Representate(IRepresentation represent, IAction action, ActionResult actionResult)
        {
            // Given the action type of representation do something
            if (action.Representable == 1)
            {
                represent.BuildView(action);
            }
            else // Not representable
            {
                represent.BuildNotification(actionResult);
            }
        }
    }
}
