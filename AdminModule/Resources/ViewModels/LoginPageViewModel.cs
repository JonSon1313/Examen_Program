using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AdminModule.Resources.Methods;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;

namespace AdminModule.Resources.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string? login;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string? password;

        public LoginPageViewModel()
        {
            Login = "";
            Password = "";
        }

        [RelayCommand(CanExecute = nameof(CanExecuteLogIn))]
        private async Task LogIn()
        {
            if (LoginCommand.Login(new()
            {
                Id = -1,
                FirstName = "",
                MiddleName = "",
                LastName = "",
                Login = Login ?? "",
                Password = Password ?? "",
                PhoneNumber = "",
                Email = ""
            },
            ConnectionCredentialsRepository.EP ??
            throw new Exception("EndPoint is Missing")))
            {
                await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                Login = "";
                Password = "";
            }
            else
            {
                WorkingObjectsRepository.Admin!.Login = "Login";
                await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                Login = "";
                Password = "";
            }
        }
        private bool CanExecuteLogIn()
        {
            return Login != "" &&
                   Password != "";
        }

        [RelayCommand]
        private async Task SignUpPageDirection()
        {
            await Shell.Current.GoToAsync($"//{nameof(SignupPage)}");
        }
    }
}
