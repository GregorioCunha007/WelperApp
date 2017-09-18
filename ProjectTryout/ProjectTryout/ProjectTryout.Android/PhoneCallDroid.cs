using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjectTryout.Model;
using Android.Telephony;
using ProjectTryout.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneCallDroid))]
namespace ProjectTryout.Droid
{
    public class PhoneCallDroid : IPhoneCall
    {
        public PhoneCallDroid()
        {
            // Empty ctor
        }

        public void MakePhoneCall(string number)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(string.Format("tel:+351[0]", number));
                var intent = new Intent(Intent.ActionCall, uri);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception e)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, args) =>
                    {
                        // User pressed Ok
                    })
                    .SetMessage(e.ToString())
                    .SetTitle("Android Exception")
                    .Show();
            }
        }
    }
}