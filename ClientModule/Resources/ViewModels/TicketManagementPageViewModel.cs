using ClientModule.Resources.Methods.Command;
using ClientModule.Resources.Models;
using ClientModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ClientModule.Resources.ViewModels
{
    public partial class TicketManagementPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<TicketNPC> tickets;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ReturnTicketsCommand))]
        TicketNPC? ticket;


        public TicketManagementPageViewModel()
        {
            Tickets = WorkingObjectsRepository.Tickets ?? [];
            Ticket = new();
            Ticket.PropertyChanged += (s, e) => 
            {
                ReturnTicketsCommand.NotifyCanExecuteChanged(); 
            };
        }

        [RelayCommand (CanExecute = nameof(CanExecute))]
        public void ReturnTickets()
        {
            ReturnTicket.Return(Ticket ?? new(),
                ConnectionCredentialsRepository.EP ?? 
                throw new Exception("Endpoint is missing"));
        }

        public bool CanExecute()
        {
            return Ticket?.Number != "";
        }

        [RelayCommand]
        public void SelectedTicketChanged()
        {

        }
    }
}