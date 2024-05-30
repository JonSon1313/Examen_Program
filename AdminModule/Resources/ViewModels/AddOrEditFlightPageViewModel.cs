using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class AddOrEditFlightPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
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
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
        private GateNPC? departureGate;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
        private GateNPC? arrivalGate;

        [ObservableProperty]
        private int departureId;

        [RelayCommand]
        private void SelectedDepartureIdChanged()
        {
            TerminalsOut = new ObservableCollection<TerminalNPC>(WorkingObjectsRepository.Terminals?
                .Where(e => e.AirportId == AirportsOut?[DepartureId].Id).ToList() ?? []);
        }

        [ObservableProperty]
        private int arrivalId;

        [RelayCommand]
        private void SelectedArrivalIdChanged()
        {
            TerminalsIn = new ObservableCollection<TerminalNPC>(WorkingObjectsRepository.Terminals?
                .Where(e => e.AirportId == AirportsIn?[ArrivalId].Id).ToList() ?? []);
        }

        [ObservableProperty]
        private int departureTerminalId;

        [RelayCommand]
        private void SelectedDepartureTerminalIdChanged()
        {
            GatesOut = new ObservableCollection<GateNPC>(WorkingObjectsRepository.Gates?
                .Where(e => e.TerminalId == TerminalsOut?[DepartureTerminalId].Id).ToList() ?? []);
        }

        [ObservableProperty]
        private int arrivalTerminalId;

        [RelayCommand]
        private void SelectedArrivalTerminalIdChanged()
        {
            GatesIn = new ObservableCollection<GateNPC>(WorkingObjectsRepository.Gates?
                .Where(e => e.TerminalId == TerminalsIn?[ArrivalTerminalId].Id).ToList() ?? []);
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

            Date= DateTime.Now;

            AirportsIn = WorkingObjectsRepository.Airports;
            AirportsOut = WorkingObjectsRepository.Airports;
            Aircrafts = WorkingObjectsRepository.Aircrafts;

            Flight!.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();

            if (ActionKey == "EDIT")
            {
                Date = Flight.DepartureTime;
                Time = Flight.DepartureTime.TimeOfDay;

                Departure = AirportsOut?.Where(e => e.Id == DepartureGate?.AirportId).FirstOrDefault();
                Arrival = AirportsOut?.Where(e => e.Id == ArrivalGate?.AirportId).FirstOrDefault();

                Aircraft = Aircrafts?.Where(e => e.Id == Flight.AircraftId).SingleOrDefault();

                DepartureTerminal = WorkingObjectsRepository.Terminals?
                    .Where(e=>e.Id == DepartureGate?.TerminalId).FirstOrDefault();
                TerminalsOut = new ObservableCollection<TerminalNPC>(WorkingObjectsRepository.Terminals?
                    .Where(e => e.AirportId == Departure?.Id).ToList() ?? []);

                ArrivalTerminal = WorkingObjectsRepository.Terminals?
                    .Where(e=>e.Id == ArrivalGate?.TerminalId).FirstOrDefault();
                TerminalsIn = new ObservableCollection<TerminalNPC>(WorkingObjectsRepository.Terminals?
                    .Where(e => e.AirportId == Arrival?.Id).ToList() ?? []);

                DepartureGate = WorkingObjectsRepository.Gates?
                    .Where(e => e.Id == Flight.FromId).FirstOrDefault();
                GatesOut = new ObservableCollection<GateNPC> (WorkingObjectsRepository.Gates?
                    .Where(e => e.AirportId == Departure?.Id).ToList() ?? []);

                ArrivalGate = WorkingObjectsRepository.Gates?
                    .Where(e => e.Id == Flight.ToId).FirstOrDefault();
                GatesIn = new ObservableCollection<GateNPC> (WorkingObjectsRepository.Gates?
                    .Where(e => e.AirportId == Arrival?.Id).ToList() ?? []);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            if (ActionKey == "ADD")
            {
                Flight!.AircraftId = Aircraft!.Id;
                Flight.ToId = ArrivalGate!.Id;
                Flight.FromId = DepartureGate!.Id;

                Flight.DepartureTime = DateTime.Parse($"{Date.ToShortDateString()} {Time}");

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
            return Flight?.Number != "" &&
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
