using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Plugin.Battery;
using ProjectTryout.Model.Utils;
using ProjectTryout.Helpers;
using Xamarin.Forms;
using Application = Android.App.Application;

namespace ProjectTryout.Droid
{
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();

            int percentage = CrossBattery.Current.RemainingChargePercent;
            if (percentage < Settings.BatteryWarningThreshold)
            {
                // Cancel alarm 
                var alarmIntent = new Intent(context, typeof(BackgroundReceiver));
                var pendingCancel = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
                AlarmManager alarmManager = context.GetSystemService(Android.Content.Context.AlarmService).JavaCast<AlarmManager>();
                alarmManager.Cancel(pendingCancel);

                MessagingCenter.Send(this, Events.LowBatteryWarning, "Battery is below " + percentage + " %");
            }
            
            wakeLock.Release();
        }
    }
}