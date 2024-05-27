using CommunityToolkit.Mvvm.ComponentModel;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using Models;
using CommunityToolkit.Mvvm.Input;
using ClientModule.Resources.Repositories;

namespace ClientModule.Resources.ViewModels
{
    public partial class OrderPageViewModel : ObservableObject
    {
        [ObservableProperty]
        AircraftNPC? aircraft;
        [ObservableProperty]
        ObservableCollection<SeatNPC>? seats;
        [ObservableProperty]
        ObservableCollection<TicketNPC>? tickets;

    }
}
