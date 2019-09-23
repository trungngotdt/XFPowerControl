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
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class AutoStart : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var service = new Intent(context, typeof(PowerStartUpService));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                context.StartForegroundService(service);
            }
            else
            {
                context.StartService(service);
            }
        }
    }
}