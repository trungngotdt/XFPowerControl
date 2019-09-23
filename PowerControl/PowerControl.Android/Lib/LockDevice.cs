using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PowerControl.Droid.Lib
{
    public class LockDevice:BasePowerControl
    {
        public LockDevice(Context context):base( context)
        {

        }

        public void StartLockDevice()
        {
            ActiveAdminDevice();
            Lock();
        }

        private void ActiveAdminDevice()
        {
            bool isAdmin = Dpm.IsAdminActive(ComDeviceAdmin);
            if (!isAdmin)
            {
                Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
                intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, ComDeviceAdmin);
                intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "Device administrator");
                Context.StartActivity(intent);
            }
        }
        private void Lock()
        {
            Dpm.LockNow();
        }

    }
}