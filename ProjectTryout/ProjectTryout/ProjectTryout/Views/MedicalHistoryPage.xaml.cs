using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTryout.Model;
using ProjectTryout.ViewModelx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectTryout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicalHistoryPage : ContentPage
    {
        public MedicalHistoryPage(IAction action)
        {
            BindingContext = new ActionRepresentationViewModel(action);
            InitializeComponent();
        }

        public ActionRepresentationViewModel MyActionRepresentationViewModel
        {
            get { return BindingContext as ActionRepresentationViewModel; }
            set { BindingContext = value; }
        }
    }
}
