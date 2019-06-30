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
using PowerControl.Droid.Services;

namespace PowerControl.Droid.BroadcastReceivers
{
    [BroadcastReceiver]
    public class PowerMenuReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var asd= PowerMenuService.instance.PerformGlobalAction(Android.AccessibilityServices.GlobalAction.PowerDialog);
            Toast.MakeText(context, "receive : "+asd, ToastLength.Short).Show();
            //PowerMenuService.instance.PerformGlobalAction(Android.AccessibilityServices.GlobalAction.PowerDialog);
        }
    }
}