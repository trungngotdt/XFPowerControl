using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using PowerControl.Droid.Services;

namespace PowerControl.Droid.Lib
{
    class MenuControlPower : BasePowerControl
    {
        public MenuControlPower(Context context) : base(context)
        {
        }

        public void OpenMenuControl()
        {
            ShutDown();
        }

        private void ShutDown()
        {

            ComponentName component = new ComponentName(Context, Java.Lang.Class.FromType(typeof(PowerMenuService)));
            Context.PackageManager.SetComponentEnabledSetting(component, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);
            Intent intent = new Intent("com.companyname.appname.ACCESSIBILITY_ACTION");
            LocalBroadcastManager.GetInstance(Context).SendBroadcast(intent);
        }

    }
}