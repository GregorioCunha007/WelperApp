using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTryout.Model;

namespace ProjectTryout.ViewModelx
{
    public class ActionRepresentationViewModel : BaseViewModel
    {
        private IAction _action;
        public IAction Action
        {
            get { return _action; }
            set { SetValue(ref _action, value);}
        }

        public ActionRepresentationViewModel(IAction a)
        {
            _action = a;
        }
    }
}
