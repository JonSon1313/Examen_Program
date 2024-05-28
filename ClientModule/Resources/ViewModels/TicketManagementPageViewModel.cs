using ClientModule.Resources.Methods;
using ClientModule.Resources.Methods.Command;
using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClientModule.Resources.ViewModels
{
    public partial class TicketManagementPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<TicketNPC> tickets;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor (nameof(ReturnTicketsCommand))]
        TicketNPC? ticket;


        public TicketManagementPageViewModel()
        {
            Tickets = WorkingObjectsRepository.Tickets;
            Ticket = new();
            Ticket.PropertyChanged += (s, e) => { ReturnTicketsCommand.NotifyCanExecuteChanged(); };
        }

        [RelayCommand (CanExecute = nameof(CanExecute))]
        
        public void ReturnTickets()
        {
            ReturnTicket.Return(ticket, ConnectionCredentialsRepository.EP);

        }

        public bool CanExecute()
        {
            return Ticket.Number != "";
        }

        [RelayCommand]
        public void SelectedTicketChanged()
        {

        }
    }
}
