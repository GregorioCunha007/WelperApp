using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akavache;
using Plugin.Toasts;
using ProjectTryout.Helpers;
using ProjectTryout.Model;
using ProjectTryout.Model.Services;
using ProjectTryout.Model.Utils;
using ProjectTryout.Views;
using Xamarin.Forms;

namespace ProjectTryout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart()
        {
            BlobCache.ApplicationName = "Welper";
            MessagingCenter.Subscribe<object, string>(this, Events.LowBatteryWarning, async (s, e) =>
            {
                BatteryVerifierService batteryVerifierService = new BatteryVerifierService();
                await batteryVerifierService.Alert(e);
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // We don't care about the return value  
            MessagingCenter.Send<App,bool>(this,Events.WaitForGps,true);
        }
    }
}
