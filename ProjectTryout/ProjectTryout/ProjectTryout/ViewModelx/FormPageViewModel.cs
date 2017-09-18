using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Plugin.Settings;
using ProjectTryout.Helpers;
using ProjectTryout.Model;
using ProjectTryout.Model.DataRepresentation;
using ProjectTryout.Model.Exceptions;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;
using ProjectTryout.Views;
using Xamarin.Forms;

namespace ProjectTryout.ViewModelx
{
    public class FormPageViewModel : BaseViewModel
    {
        private readonly IAppNavigation _navigation;

        private int [] array = {25, 30, 35, 40};

        private int _batteryWarningPercentageThreshold;

        public int BatteryWarningPercentageThreshold
        {
            get { return _batteryWarningPercentageThreshold; }
            set { SetValue(ref _batteryWarningPercentageThreshold, value); }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }

        public FormPageViewModel(IAppNavigation navigation) : base(navigation)
        {
            FinishCommand = new Command(Finish);
            _navigation = navigation;
        }

        public ICommand FinishCommand { get; private set; }

        private async void Finish()
        {
            var data = new { Password = Password };
            try
            {
                Result result = await HttpMiddleman.VerifyPassword(Id, data);

                Settings.UserId = Id;
                Settings.Password = Password;
                Settings.BatteryWarningThreshold = array[BatteryWarningPercentageThreshold];

                await _navigation.GoToCardExhibitPage();
                BuildToast(true, "User authenticaded");

            }
            catch(PasswordMismatchException e)
            {
                Debug.WriteLine(e.Message);
                BuildToast(false, "Bad password");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                BuildToast(false,"Unexpected Exception");
            }
        }
    }
}
