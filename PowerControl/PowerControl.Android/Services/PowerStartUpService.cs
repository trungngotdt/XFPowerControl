using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace PowerControl.Droid.Services
{
    [Service(Name = "com.companyname.powerapp.powerStartUpService")]
    class PowerStartUpService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            StartForeground(100, CreateNotification(content: "Power Panel"));
            base.OnCreate();
        }
        private PendingIntent CreatePowerIntent(bool isLockDeviceAction,int resquestCode)
        {
            var actionPowerUp = new Intent(this, typeof(PowerControlService));
            var bundle = new Bundle();
            bundle.PutBoolean("IsLockDeviceAction",isLockDeviceAction);
            actionPowerUp.PutExtras(bundle);
            PendingIntent actionIntent = PendingIntent.GetService(this, resquestCode, actionPowerUp, PendingIntentFlags.UpdateCurrent);
            return actionIntent;
        }

        private Notification CreateNotification(string content = "Content", int icon = -1)
        {
            NotificationCompat.Builder builder;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                if (notificationManager.GetNotificationChannel("remoteTracker") == null)
                {
                    var chanel = new NotificationChannel("powerStartUp", "Power", NotificationImportance.High) { Description = "Service Power StartUp" };
                    notificationManager.CreateNotificationChannel(chanel);
                }
                var actionLockDevice = CreatePowerIntent(true,1);
                var actionShowMenu = CreatePowerIntent(false,2);
                builder = new NotificationCompat.Builder(this, "powerStartUp").SetPriority(1)
                    .AddAction(Resource.Mipmap.ic_launcher,"Lock",actionLockDevice)
                    .AddAction(Resource.Mipmap.ic_launcher, "Shut Down Menu", actionShowMenu)
                    .SetContentText(content).SetContentTitle("Power App");
            }
            else
            {
                builder = new NotificationCompat.Builder(this).SetContentTitle("Power Up").SetContentText("Please update to Android O");
            }
            if (icon == -1)
            {
                builder.SetSmallIcon(Resource.Mipmap.ic_launcher);
            }
            else
            {
                builder.SetSmallIcon(icon);
            }
            return builder.Build();
        }
    }
}