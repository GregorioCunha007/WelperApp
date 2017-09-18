using Android.Content;
using Android.Locations;
using ProjectTryout.Droid;
using ProjectTryout.Model.Services;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(EnableLocationServiceImplementation))]
namespace ProjectTryout.Droid
{
    class EnableLocationServiceImplementation : IEnableLocationService
    {
        public void EnableLocationService()
        {
            Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(intent);           
        }

        public bool IsLocationServiceEnabled()
        {
            LocationManager lm = LocationManager.FromContext(Application.Context);
            return lm.IsProviderEnabled(LocationManager.GpsProvider);
        }
    }
}