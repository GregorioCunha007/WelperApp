using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectTryout.ViewModelx;
using ProjectTryout.Model.Services;

namespace ProjectTryout.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        public FormPage()
        {
            InitializeComponent();
            BindingContext = new FormPageViewModel(new PageService());
        }

        public FormPageViewModel MyFormPageViewModel
        {
            get { return BindingContext as FormPageViewModel; }
            set { BindingContext = value; }
        }

    }
}

