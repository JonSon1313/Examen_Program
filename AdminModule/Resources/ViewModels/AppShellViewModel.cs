using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdminModule.Resources.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private AdministratorNPC? admin;

        public AppShellViewModel()
        {
            Admin = WorkingObjectsRepository.Admin;
        }

        [RelayCommand]
        private async Task Logout()
        {
            Admin = null;
            WorkingObjectsRepository.AdminLogoff();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
