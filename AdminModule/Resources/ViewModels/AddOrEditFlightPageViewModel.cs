using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class AddOrEditFlightPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private FlightNPC? flight;
        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airports;

        [ObservableProperty]
        private AirportNPC? departure;
        [ObservableProperty]
        private AirportNPC? arrival;
        
        [ObservableProperty]
        private ObservableCollection<AircraftNPC>? aircrafts;
        [ObservableProperty]
        private AircraftNPC? aircraft;

        [ObservableProperty]
        private string actionKey;

        public AddOrEditFlightPageViewModel()
        {
            ActionKey = WorkingObjectsRepository.Action;

            Flight = WorkingObjectsRepository.WorkObject as FlightNPC;
            Airports = WorkingObjectsRepository.Airports;
            Aircrafts = WorkingObjectsRepository.Aircrafts;

            Flight!.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();

            if (ActionKey == "EDITFLIGHT")
            {
                Departure = Airports?.Where(e => e.Id == Flight.FromId).SingleOrDefault();
                Arrival = Airports?.Where(e => e.Id == Flight.ToId).SingleOrDefault();
                Aircraft = Aircrafts?.Where(e => e.Id == Flight.AircraftId).SingleOrDefault();
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            if (ActionKey == "ADDFLIGHT")
            {
                Flight.AircraftId = Aircraft.Id;
                Flight.ToId = Arrival.Id;
                Flight.FromId = Departure.Id;
                if (AddCommand.Add(Flight?.ConvertToFlight() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                {
                    Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                    Flight = new();
                }
            }
            else
            {
                if (EditCommand.Edit(Flight?.ConvertToFlight() ?? new(),
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("EndPoint is Missing")))
                    Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
            }
        }
        private bool CanExecuteAction()
        {
            return Flight?.Number != null &&
                   Flight?.BasePrice != 0 &&
                   Flight?.DepartureTime != null &&
                   Departure != null &&
                   Arrival != null &&
                   Airports != null;
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
