using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using ButtonCircle.FormsPlugin.Droid;
using ImageCircle.Forms.Plugin.Droid;
using PowerControl.Droid.Lib;
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
        private LockDevice Lock;
        private MenuControlPower MenuControlPower;
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
            InitPowerControl();
            CreateMessageCenter();
        }

        private void InitPowerControl()
        {
            Lock = new LockDevice(this);
            MenuControlPower = new MenuControlPower(this);
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

                   
                    Lock.StartLockDevice();
                    //ActiveAdminDevice();
                    //LockDeivce();
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
                    MenuControlPower.OpenMenuControl();
                    //ActiveAdminDevice();
                    //ShutDown();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }

            });
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

