using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProjectTryout.Model;

namespace ProjectTryout.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string UserIdKey = "username_key";
        private static readonly int UserIdDefault = -1;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordDefault = string.Empty;

        private const string TimeStampPrevKey = "timestampprev_key";
        private static DateTime TimeStampPrevDefault = default(DateTime);

        private const string BatteryWarningThresholdKey = "batterywarning_key";
        private static readonly int BatteryWarningThresholdDefault = -1;

        public static int UserId
        {
            get { return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(UserIdKey, value); }
        }

        public static string Password
        {
            get { return AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault); }
            set { AppSettings.AddOrUpdateValue(PasswordKey, value); }
        }

        public static DateTime TimeStampPrev
        {
            get { return AppSettings.GetValueOrDefault(TimeStampPrevKey, TimeStampPrevDefault); }
            set { AppSettings.AddOrUpdateValue(TimeStampPrevKey, value); }
        }

        public static int BatteryWarningThreshold
        {
            get { return AppSettings.GetValueOrDefault(BatteryWarningThresholdKey, BatteryWarningThresholdDefault); }
            set { AppSettings.AddOrUpdateValue(BatteryWarningThresholdKey, value); }
        }
    }
}
