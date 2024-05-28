using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace ClientModule.Resources.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private ClientNPC? client;

        public AppShellViewModel()
        {
            Client = WorkingObjectsRepository.Client;
        }

        [RelayCommand]
        private async Task Logout()
        {
            Client = null;
            WorkingObjectsRepository.ClientLogoff();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        [RelayCommand]
        private void RedirectToSite()
        {
            string target = "http://127.0.0.1:5500/HomeWorkProject/index.html";
            if (DeviceInfo.Current.Platform != DevicePlatform.iOS && DeviceInfo.Current.Platform != DevicePlatform.macOS)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = target,
                    UseShellExecute = true
                });
            }
        }
    }
}
