using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class ClientNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string firstName;
        [ObservableProperty]
        private string middleName;
        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string login;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string phoneNumber;
        [ObservableProperty]
        private string email;

        public void ConvertFromClient(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            MiddleName = client.MiddleName;
            LastName = client.LastName;
            Login = client.Login;
            Password = client.Password;
            PhoneNumber = client.PhoneNumber;
            Email = client.Email;
        }
        public Client ConvertToClient()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Login = Login,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
        }
    }
}
