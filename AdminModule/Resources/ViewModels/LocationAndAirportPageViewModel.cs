using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using AdminModule.Resources.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class LocationAndAirportPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLocation;
        [ObservableProperty]
        private bool isAirport;

        [ObservableProperty]
        private ObservableCollection<CountryNPC>? countries;
        [ObservableProperty]
        private CountryNPC? country;

        [RelayCommand]
        private void SelectedCountryChanged()
        {
            if (Country != null)
            {
                Cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities?
                    .Where(c => c.CountryId == Country?.Id).ToList() ?? []);
            }
        }

        [ObservableProperty]
        private ObservableCollection<CityNPC>? cities;
        [ObservableProperty]
        private CityNPC? city;

        [RelayCommand]
        private void SelectedCityChanged() { }

        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airports;
        [ObservableProperty]
        private AirportNPC? airport;

        [RelayCommand]
        private void SelectedAirportChanged()
        {
            if (Airport != null)
            {
                Terminals = new ObservableCollection<TerminalNPC>(WorkingObjectsRepository.Terminals?
                    .Where(c => c.AirportId == Airport?.Id).ToList() ?? []);
            }
        }

        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminals;
        [ObservableProperty]
        private TerminalNPC? terminal;

        [RelayCommand]
        private void SelectedTerminalChanged()
        {
            if (Terminal != null)
            {
                Gates = new ObservableCollection<GateNPC>(WorkingObjectsRepository.Gates?
                    .Where(c => c.TerminalId == Terminal?.Id).ToList() ?? []);
            }
        }

        [ObservableProperty]
        private ObservableCollection<GateNPC>? gates;
        [ObservableProperty]
        private GateNPC? gate;

        [RelayCommand]
        private void SelectedGateChanged() { }

        public LocationAndAirportPageViewModel()
        {
            IsLocation = true;
            IsAirport = false;

            Countries = WorkingObjectsRepository.Countries;
            Country = new();
            Country.PropertyChanged += (s, e) =>
            {
                AddCityCommand.NotifyCanExecuteChanged();
            };

            Airport = new();
            Airports = WorkingObjectsRepository.Airports;
            Airport.PropertyChanged += (s, e) =>
            {
                AddTerminalCommand.NotifyCanExecuteChanged();
            };

            Terminal = new();
            Terminal.PropertyChanging += (s, e) =>
            {
                AddGateCommand.NotifyCanExecuteChanged();
            };
        }

        [RelayCommand]
        private async Task AddAirport()
        {
            WorkingObjectsRepository.Action = "ADD";
            WorkingObjectsRepository.WorkObject = new AirportNPC();

            await Shell.Current.GoToAsync(nameof(AddOrEditAirportPage));
        }
        //


        [RelayCommand (CanExecute = nameof(CanExecuteAddTerminal))]
        private async Task AddTerminal()
        {
            WorkingObjectsRepository.Action = "ADDTERMINAL";
            WorkingObjectsRepository.WorkObject = new TerminalNPC() { AirportId = Airport.Id };

            await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
        }
        public bool CanExecuteAddTerminal()
        {
            return Airport?.FullName != ""; 
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAddGate))]
        public async Task AddGate()
        {
            WorkingObjectsRepository.Action = "ADDGATE";
            WorkingObjectsRepository.WorkObject = new GateNPC()
            {
                TerminalId = Terminal.Id,
                AirportId = Terminal.AirportId
            };

            await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
        }
        public bool CanExecuteAddGate()
        {
            return Airport?.FullName != "" &&
                Terminal?.Name != "";
        }

        [RelayCommand]
        private async Task AddCountry()
        {
            WorkingObjectsRepository.Action = "ADDCOUNTRY";
            WorkingObjectsRepository.WorkObject = new CountryNPC();

            await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAddCity))]
        private async Task AddCity()
        {
            WorkingObjectsRepository.Action = "ADDCITY";
            WorkingObjectsRepository.WorkObject = new CityNPC() { CountryId = Country.Id };

            await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
        }
        public bool CanExecuteAddCity()
        {
            return Country?.Name != "";
        }

        [RelayCommand]
        public void ActivateLocation()
        {
            IsLocation = true;
            IsAirport = false;
        }
        [RelayCommand]
        public void ActivateAirport()
        {
            IsAirport = true;
            IsLocation = false;
        }


        //Write code Behind
        [RelayCommand]
        private void Delete(object obj)
        {
            var success = false;

            if (obj is CountryNPC)
                success = RemoveCommand.Remove(new Country(), ((CountryNPC)obj).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is CityNPC)
                success = RemoveCommand.Remove(new City(), ((CityNPC)obj).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is AirportNPC)
                success = RemoveCommand.Remove(new Airport(), ((AirportNPC)obj).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is TerminalNPC)
                success = RemoveCommand.Remove(new Terminal(), ((TerminalNPC)obj).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is GateNPC)
                success = RemoveCommand.Remove(new Gate(), ((GateNPC)obj).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));

            if(success)
                Shell.Current.DisplayAlert("Success", "Object has been successfully deleted", "Ok");
        }
        [RelayCommand]
        private async Task Modify(object obj)
        {
            if (obj is CountryNPC)
                WorkingObjectsRepository.Action = "EDITCOUNTRY";
            else if (obj is CityNPC)
                WorkingObjectsRepository.Action = "EDITCITY";
            else if (obj is TerminalNPC)
                WorkingObjectsRepository.Action = "EDITTERMINAL";
            else if (obj is GateNPC)
                WorkingObjectsRepository.Action = "EDITGATE";
            else if (obj is AirportNPC)
                WorkingObjectsRepository.Action = "EDITAIRPORT";

            WorkingObjectsRepository.WorkObject = obj;

            if (obj is not AirportNPC)
                await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
            else
                await Shell.Current.GoToAsync(nameof(AddOrEditAirportPage));
        }
    }
}
