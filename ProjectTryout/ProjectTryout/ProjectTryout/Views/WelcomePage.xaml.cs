using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectTryout.Model.Services;
using ProjectTryout.ViewModelx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectTryout.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage 
    {
        public WelcomePage()
        {
            InitializeComponent();
            BindingContext = new WelcomePageViewModel(new PageService());
        }

        public WelcomePageViewModel MyWelcomePageViewModel
        {
            get { return BindingContext as WelcomePageViewModel; }
            set { BindingContext = value; }
        }
    }
}
