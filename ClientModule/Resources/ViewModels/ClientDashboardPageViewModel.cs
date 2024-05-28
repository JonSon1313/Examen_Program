using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ClientModule.Resources.ViewModels
{
    public partial class ClientDashboardPageViewModel : ObservableObject
    {
        public ClientDashboardPageViewModel()
        {
            WorkingObjectsRepository.ClientLoggedIn();
        }
    }
}
