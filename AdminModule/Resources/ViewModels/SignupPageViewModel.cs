using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.ComponentModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class SignupPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private AdministratorNPC? admin;
        private void Admin_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SignUpCommand.NotifyCanExecuteChanged();
        }

        public SignupPageViewModel()
        {
            Admin = new()
            {
                Id = -1,
                FirstName = "",
                MiddleName = "",
                LastName = "",
                Login = "",
                Password = "",
                PhoneNumber = "",
                Email = ""
            };
            Admin.PropertyChanged += Admin_PropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteSignup))]
        private async Task SignUp()
        {
            if (SignupCommand.Signup(Admin?.ConvertToAdministrator() ?? new(),
            ConnectionCredentialsRepository.EP ??
            throw new Exception("EndPoint is Missing")))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
        public bool CanExecuteSignup()
        {
            return Admin?.FirstName != "" &&
                   Admin?.MiddleName != "" &&
                Admin?.LastName != "" &&
                Admin?.Login != "" &&
                Admin?.Password != "" &&
                Admin?.PhoneNumber != "" &&
                CheckPhoneNumber.Check(Admin?.PhoneNumber ?? "") &&
                Admin?.Email != "" &&
                CheckEmailAddress.Check(Admin?.Email ?? "");
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
