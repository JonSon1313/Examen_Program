using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace AdminModule.Resources.ViewModels
{
    public partial class FlightPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<FlightNPC>? flights;

        [RelayCommand]
        private void SelectedFlightChanged()
        {
            Seats = [];
            if(Flight != null)
            {
                var temp = WorkingObjectsRepository.Seats?
                    .Where(e => e.FlightId == Flight.Id).ToList();
                for (var i = 0; i < temp?.Count; i++)
                    Seats.Add(temp[i]);
            }
            else
                Seats = WorkingObjectsRepository.Seats;
        }
        
        [ObservableProperty]
        private FlightNPC? flight;
        
        [ObservableProperty]
        private ObservableCollection<SeatTypeNPC>? seatTypes;

        [RelayCommand]
        private void SelectedSeatTypeChanged()
        {
            Seats = [];
            if (SeatType != null)
            {
                var temp = WorkingObjectsRepository.Seats?
                    .Where(e => e.SeatTypeId == SeatType.Id).ToList();
                for (var i = 0; i < temp?.Count; i++)
                    Seats.Add(temp[i]);
            }
            else
                Seats = WorkingObjectsRepository.Seats;
        }

        [ObservableProperty]
        private SeatTypeNPC? seatType;
        
        [ObservableProperty]
        private ObservableCollection<SeatNPC>? seats;

        [RelayCommand]
        private void SelectedSeatChanged() { }

        [ObservableProperty]
        private SeatNPC? seat;

        public FlightPageViewModel()
        {
            Flights = WorkingObjectsRepository.Flights;
            Seats = WorkingObjectsRepository.Seats;
            SeatTypes = WorkingObjectsRepository.SeatTypes;
        }

        [RelayCommand]
        private void Delete(object obj)
        {
            var success = false;
            if (obj is FlightNPC)
                success = RemoveCommand.Remove(new Flight(), (obj as FlightNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is SeatTypeNPC)
                success = RemoveCommand.Remove(new SeatType(), (obj as SeatTypeNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            if (success)
            {
                Shell.Current.DisplayAlert("Success", "Object has been successfully deleted", "Ok");
            }
        }

        [RelayCommand]
        private async Task Modify(object obj)
        {
            WorkingObjectsRepository.WorkObject = obj;

            if (obj is FlightNPC)
            {
                WorkingObjectsRepository.Action = "EDITFLIGHT";
                await Shell.Current.GoToAsync(nameof(AddOrEditFlightPage));
            }
            else if (obj is SeatTypeNPC)
            {
                WorkingObjectsRepository.Action = "EDITSEATTYPE";
                await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
            }
        }

        [RelayCommand]
        private async Task AddSeatType()
        {
            WorkingObjectsRepository.Action = "ADDSEATTYPE";
            WorkingObjectsRepository.WorkObject = new SeatTypeNPC();

            await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
        }
        
        [RelayCommand]
        private async Task AddFlight()
        {
            WorkingObjectsRepository.Action = "ADDFLIGHT";
            WorkingObjectsRepository.WorkObject = new FlightNPC();

            await Shell.Current.GoToAsync(nameof(AddOrEditFlightPage));
        }
    }
}
