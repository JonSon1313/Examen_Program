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
            Cities = [];
            if (Country != null)
            {
                var temp = WorkingObjectsRepository.Cities?.Where(c => c.CountryId == Country?.Id).ToList();
                for (var i = 0; i < temp?.Count; i++)
                    Cities.Add(temp[i]);
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
            Terminals = [];
            if (Airport != null)
            {
                var temp = WorkingObjectsRepository.Terminals?.Where(c => c.AirportId == Airport?.Id).ToList();
                for (var i = 0; i < temp?.Count; i++)
                    Terminals.Add(temp[i]);
            }
        }

        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminals;
        [ObservableProperty]
        private TerminalNPC? terminal;

        [RelayCommand]
        private void SelectedTerminalChanged()
        {
            Gates = [];
            if (Terminal != null)
            {
                var temp = WorkingObjectsRepository.Gates?.Where(c => c.TerminalId == Terminal?.Id).ToList();
                for (var i = 0; i < temp?.Count; i++)
                    Gates.Add(temp[i]);
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
            Airports = WorkingObjectsRepository.Airports;
        }

        //Currently is nothing here
        [RelayCommand]
        private void AddAirport()
        { }
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
            return Airport != null; 
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
            return Airport != null &&
                Terminal != null; 
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
            return Country != null;
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
                success = RemoveCommand.Remove(new Country(), (obj as CountryNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is CityNPC)
                success = RemoveCommand.Remove(new City(), (obj as CityNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is AirportNPC)
                success = RemoveCommand.Remove(new Airport(), (obj as AirportNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is TerminalNPC)
                success = RemoveCommand.Remove(new Terminal(), (obj as TerminalNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));
            else if (obj is GateNPC)
                success = RemoveCommand.Remove(new Gate(), (obj as GateNPC).Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("Error"));

            if(success)
            {
                Shell.Current.DisplayAlert("Success", "Object has been successfully deleted", "Ok");
            }
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

            if (!(obj is AirportNPC))
                await Shell.Current.GoToAsync(nameof(SmallObjectAddOrEditLocationAndAirportPage));
            else
            {
                
            }
        }
    }
}
