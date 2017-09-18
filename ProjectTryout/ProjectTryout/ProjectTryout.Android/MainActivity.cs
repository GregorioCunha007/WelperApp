using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Plugin.Messaging;
using Plugin.Permissions;
using Plugin.Toasts;
using Xamarin.Forms;

namespace ProjectTryout.Droid
{
    [Activity(Label = "Welper", Icon = "@drawable/icon_w", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            //Auto dial enable 
            CrossMessaging.Current.Settings().Phone.AutoDial = true;

            //Cache initialization
            CachedImageRenderer.Init();
            //Toast Plugin initializion
            DependencyService.Register<ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            // Battery broadcast 
            SetUpBatteryBroadcast();
        }

        public void SetUpBatteryBroadcast()
        {
            var alarmIntent = new Intent(this, typeof(BackgroundReceiver));
            var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();
            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 3 * 1000, 500000, pending);
        }

        // Necessary for Geolocation
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

