using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using ButtonCircle.FormsPlugin.Droid;
using ImageCircle.Forms.Plugin.Droid;
using PowerControl.Droid.Services;
using Prism;
using Prism.Ioc;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PowerControl.Droid
{
    [Activity(Label = "PowerControl", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private ComponentName comDeviceAdmin;
        private DevicePolicyManager dpm;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            ButtonCircleRenderer.Init();
            LoadApplication(new App(new AndroidInitializer()));
            CreateDeviceManager();
            CreateMessageCenter();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CreateMessageCenter()
        {
            MessagingCenter.Subscribe<List<int>>(this, "Lock", async l =>
            {
                try
                {
                    ActiveAdminDevice();
                    LockDeivce();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }

            });
            MessagingCenter.Subscribe<List<int>>(this, "ShutDown", async l =>
            {
                try
                {

                    //ActiveAdminDevice();
                    ShutDown();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }

            });
        }

        private void CreateDeviceManager()
        {
            dpm = (DevicePolicyManager)GetSystemService(DevicePolicyService);
            comDeviceAdmin = new ComponentName(this, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
        }
        private void ActiveAdminDevice()
        {
            bool isAdmin = dpm.IsAdminActive(comDeviceAdmin);
            if (!isAdmin)
            {
                Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
                intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, comDeviceAdmin);
                intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "Device administrator");
                StartActivity(intent);
            }
        }

        private void ShutDown()
        {

            ComponentName component = new ComponentName(ApplicationContext, Java.Lang.Class.FromType(typeof(PowerMenuService)));
            PackageManager.SetComponentEnabledSetting(component, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);
            Intent intent = new Intent("com.companyname.appname.ACCESSIBILITY_ACTION");
            var power = Android.AccessibilityServices.GlobalAction.PowerDialog;
            //intent.PutExtra("action", 6);
            //intent.PutExtra("action",Android.AccessibilityServices.GlobalAction.PowerDialog);
            LocalBroadcastManager.GetInstance(ApplicationContext).SendBroadcast(intent);
            //dpm.Reboot(comDeviceAdmin);
        }

        private void LockDeivce()
        {
            dpm.LockNow();
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

