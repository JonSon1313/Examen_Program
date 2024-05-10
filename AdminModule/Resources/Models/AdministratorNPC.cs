using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace AdminModule.Resources.Models
{
    public partial class AdministratorNPC : ObservableObject
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

        public void ConvertFromAdministrator(Administrator admin)
        {
            Id = admin.Id;
            FirstName = admin.FirstName;
            MiddleName = admin.MiddleName;
            LastName = admin.LastName;
            Login = admin.Login;
            Password = admin.Password;
            PhoneNumber = admin.PhoneNumber;
            Email = admin.Email;
        }
        public Administrator ConvertToAdministrator()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Login = Login,
                Password = Password,
            };
        }

    }
}
