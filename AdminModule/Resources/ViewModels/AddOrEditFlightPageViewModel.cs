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
        private DateTime date;
        [ObservableProperty]
        private TimeSpan time;

        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airportsIn;
        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminalsIn;
        [ObservableProperty]
        private ObservableCollection<GateNPC>? gatesIn;
        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airportsOut;
        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminalsOut;
        [ObservableProperty]
        private ObservableCollection<GateNPC>? gatesOut;

        [ObservableProperty]
        private AirportNPC? departure;
        [ObservableProperty]
        private AirportNPC? arrival;
        [ObservableProperty]
        private TerminalNPC? departureTerminal;
        [ObservableProperty]
        private TerminalNPC? arrivalTerminal;
        [ObservableProperty]
        private GateNPC? departureGate;
        [ObservableProperty]
        private GateNPC? arrivalGate;

        [ObservableProperty]
        private int departureId;
        [RelayCommand]
        private void SelectedDepartureIdChanged()
        {
            TerminalsOut = [];
            var temp = WorkingObjectsRepository.Terminals?
                .Where(e => e.AirportId == AirportsOut?[DepartureId].Id).ToList();
            for (int i = 0; i < temp?.Count; i++)
                TerminalsOut?.Add(temp[i]);
        }

        [ObservableProperty]
        private int arrivalId;
        [RelayCommand]
        private void SelectedArrivalIdChanged()
        {
            TerminalsIn = [];
            var temp = WorkingObjectsRepository.Terminals?
                .Where(e => e.AirportId == AirportsIn?[ArrivalId].Id).ToList();
            for (int i = 0; i < temp?.Count; i++)
                TerminalsIn?.Add(temp[i]);
        }

        [ObservableProperty]
        private int departureTerminalId;
        [RelayCommand]
        private void SelectedDepartureTerminalIdChanged()
        {
            if (DepartureTerminalId >= 0)
            {
            GatesOut = [];
            var temp = WorkingObjectsRepository.Gates?
                .Where(e => e.TerminalId == TerminalsOut?[DepartureTerminalId].Id).ToList();
            for (int i = 0; i < temp?.Count; i++)
                GatesOut?.Add(temp[i]);
            }
        }

        [ObservableProperty]
        private int arrivalTerminalId;
        [RelayCommand]
        private void SelectedArrivalTerminalIdChanged()
        {
            GatesIn = [];
            var temp = WorkingObjectsRepository.Gates?
                .Where(e => e.TerminalId == TerminalsIn?[ArrivalTerminalId].Id).ToList();
            for (int i = 0; i < temp?.Count; i++)
                GatesIn?.Add(temp[i]);
        }
        
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
            AirportsIn = WorkingObjectsRepository.Airports;
            AirportsOut = WorkingObjectsRepository.Airports;
            Aircrafts = WorkingObjectsRepository.Aircrafts;

            Flight!.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();

            if (ActionKey == "EDIT")
            {
                Date = Flight.DepartureTime;
                Time = Flight.DepartureTime.TimeOfDay;

                Aircraft = Aircrafts?.Where(e => e.Id == Flight.AircraftId).SingleOrDefault();

                DepartureGate = WorkingObjectsRepository.Gates?.Where(e => e.Id == Flight.FromId).FirstOrDefault();
                ArrivalGate = WorkingObjectsRepository.Gates?.Where(e => e.Id == Flight.ToId).FirstOrDefault();

                DepartureTerminal = WorkingObjectsRepository.Terminals?.Where(e=>e.Id == DepartureGate?.TerminalId).FirstOrDefault();
                ArrivalTerminal = WorkingObjectsRepository.Terminals?.Where(e=>e.Id == ArrivalGate?.TerminalId).FirstOrDefault();
                
                Departure = AirportsOut?.Where(e => e.Id == DepartureGate?.AirportId).FirstOrDefault();
                Arrival = AirportsOut?.Where(e => e.Id == ArrivalGate?.AirportId).FirstOrDefault();
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            if (ActionKey == "ADD")
            {
                Flight.AircraftId = Aircraft.Id;
                Flight.ToId = Arrival.Id;
                Flight.FromId = Departure.Id;
                Flight.DepartureTime = DateTime.Parse($"{Date.ToShortDateString()} {Time}");
                Console.WriteLine($"{Time}");
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
                   DepartureGate?.Name != "" &&
                   ArrivalGate?.Name != "";
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
