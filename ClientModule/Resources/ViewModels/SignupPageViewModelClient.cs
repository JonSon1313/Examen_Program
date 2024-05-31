using ClientModule.Resources.Methods;
using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientModule.Resources.ViewModels
{
    public partial class SignupPageViewModelClient : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private ClientNPC? client;

        private void CreateNewUser()
        {
            Client = new()
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

        public SignupPageViewModelClient()
        {
            CreateNewUser();
            if (Client != null)
            {
                Client.PropertyChanged += (s, e) =>
                {
                    SignUpCommand.NotifyCanExecuteChanged();
                };
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteSignup))]
        private void SignUp()
        {
            if (SignupCommand.Signup(Client?.ConvertToClient() ?? new(),
            ConnectionCredentialsRepository.EP ??
            throw new Exception("EndPoint is Missing")))
            {
               Shell.Current.GoToAsync($"//{nameof(ClientDashboardPage)}");
                CreateNewUser();
            }
            else
            {
                CreateNewUser();
            }
        }
        public bool CanExecuteSignup()
        {
            return Client?.FirstName != "" &&
                   Client?.MiddleName != "" &&
                   Client?.LastName != "" &&
                   Client?.Login != "" &&
                   Client?.Password != "" &&
                   Client?.PhoneNumber != "" &&
                   CheckPhoneNumber.Check(Client?.PhoneNumber ?? "") &&
                   Client?.Email != "" &&
                   CheckEmailAddress.Check(Client?.Email ?? "");
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}