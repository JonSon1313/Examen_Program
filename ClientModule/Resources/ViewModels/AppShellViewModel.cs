using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    }
}
