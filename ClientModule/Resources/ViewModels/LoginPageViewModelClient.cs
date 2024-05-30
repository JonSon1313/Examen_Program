using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClientModule.Resources.Methods;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;

namespace ClientModule.Resources.ViewModels
{
    public partial class LoginPageViewModelClient : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string? login;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string? password;

        public LoginPageViewModelClient()
        {
            Login = "";
            Password = "";
        }

        [RelayCommand(CanExecute = nameof(CanExecuteLogIn))]
        private void LogIn()
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
                Shell.Current.GoToAsync($"//{nameof(ClientDashboardPage)}");
                Login = "";
                Password = "";
            }
            else
            {
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
