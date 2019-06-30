using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using PowerControl.Droid.BroadcastReceivers;

namespace PowerControl.Droid.Services
{
    [Service(Name = "com.companyname.appname.service",Enabled =true,Exported =true,Permission =Manifest.Permission.BindAccessibilityService)]
    [MetaData("android.accessibilityservice",Resource = "@xml/accessibility_service")]
    [IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
    public class PowerMenuService : AccessibilityService
    {
        public static PowerMenuService instance;
        private BroadcastReceiver broadcastReceiver = new PowerMenuReceiver();
        public override void OnAccessibilityEvent(AccessibilityEvent e)
        {
        }
        public override void OnInterrupt()
        {
        }
        protected override void OnServiceConnected()
        {
            base.OnServiceConnected();
            instance = this;
        }
        
        public override void OnCreate()
        {
            
            base.OnCreate();
            Toast.MakeText(this, "create", ToastLength.Short).Show();
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(broadcastReceiver, new IntentFilter("com.companyname.appname.ACCESSIBILITY_ACTION"));
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            Toast.MakeText(this, "destroy", ToastLength.Short).Show();
            LocalBroadcastManager.GetInstance(this).UnregisterReceiver(broadcastReceiver);
        }

    }
}