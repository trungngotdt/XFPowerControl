using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using PowerControl.Droid.Lib;

namespace PowerControl.Droid.Services
{

    [Service(Name = "com.companyname.powerapp.powerControlService")]
    public class PowerControlService : Service
    {
        private LockDevice Lock;
        private MenuControlPower MenuControlPower;
        private ComponentName comDeviceAdmin;
        private DevicePolicyManager dpm;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnCreate()
        {
            base.OnCreate();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            var bundle = intent.Extras;
            var isLockDeviceAction = bundle.GetBoolean("IsLockDeviceAction");


            if (isLockDeviceAction)
            {
                Lock = new LockDevice(this);
                Lock.StartLockDevice();
            }
            else
            {
                MenuControlPower = new MenuControlPower(this);
                MenuControlPower.OpenMenuControl();
            }
            return StartCommandResult.NotSticky;
        }

    }
}