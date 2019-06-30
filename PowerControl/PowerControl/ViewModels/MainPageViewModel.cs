using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PowerControl.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand btnCommandLockDevice;
        private DelegateCommand btnCommandShowMenuPower;
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
        public DelegateCommand BtnCommandLockDevice { get => btnCommandLockDevice ?? new DelegateCommand(() => { LockDevice(); }); }
        public DelegateCommand BtnCommandShowMenuPower { get => btnCommandShowMenuPower ?? new DelegateCommand(() => { ShowMenuPower(); }); }
        private void LockDevice()
        {
            try
            {
                MessagingCenter.Send(new List<int>() { 10 }, "Lock");
                //DeviceDisplay.KeepScreenOn = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ShowMenuPower()
        {
            try
            {
                MessagingCenter.Send(new List<int>() { 10 }, "ShutDown");
                //DeviceDisplay.KeepScreenOn = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
