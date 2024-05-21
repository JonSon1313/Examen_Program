using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class SignupPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private AdministratorNPC? admin;

        private void CreateNewUser()
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
        }

        public SignupPageViewModel()
        {
            CreateNewUser();
            if (Admin != null)
            {
                Admin.PropertyChanged += (s, e) =>
                {
                    SignUpCommand.NotifyCanExecuteChanged();
                };
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteSignup))]
        private void SignUp()
        {
            if (SignupCommand.Signup(Admin?.ConvertToAdministrator() ?? new(),
            ConnectionCredentialsRepository.EP ??
            throw new Exception("EndPoint is Missing")))
            {
                Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                CreateNewUser();
            }
            else
            {
                CreateNewUser();
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
