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
using PowerControl.Droid.Services;

namespace PowerControl.Droid.Lib
{
    public class BasePowerControl
    {
        private ComponentName comDeviceAdmin;
        private DevicePolicyManager dpm;
        private ContextWrapper applicationContext;
        private Context context;
        public DevicePolicyManager Dpm { get => dpm;  }
        public ComponentName ComDeviceAdmin { get => comDeviceAdmin;  }
        public Context Context { get => context;}

        public BasePowerControl( Context context)
        {
            this.context = context;
            CreateDeviceManager();
        }

        private void CreateDeviceManager()
        {
            dpm =(DevicePolicyManager)context.GetSystemService(Context.DevicePolicyService);
            comDeviceAdmin = new ComponentName(context, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
        }
    }
}