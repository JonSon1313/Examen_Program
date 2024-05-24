using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class SmallObjectAddOrEditLocationAndAirportPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isCountry;
        [ObservableProperty]
        private bool isCity;
        [ObservableProperty]
        private bool isTerminal;
        [ObservableProperty]
        private bool isGate;
        [ObservableProperty]
        private bool isSeatType;

        [ObservableProperty]
        private ObservableCollection<CountryNPC>? countries;
        [ObservableProperty]
        private CountryNPC? country;
        [ObservableProperty]
        private CityNPC? city;

        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airports;
        [ObservableProperty]
        private AirportNPC? airport;

        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminals;
        [ObservableProperty]
        private TerminalNPC? terminal;

        [ObservableProperty]
        private GateNPC? gate;

        [ObservableProperty]
        private SeatTypeNPC? seatType;

        [ObservableProperty]
        private string actionKey;

        public SmallObjectAddOrEditLocationAndAirportPageViewModel()
        {
            ActionKey = WorkingObjectsRepository.Action;

            switch (ActionKey)
            {
                case "ADDCOUNTRY":
                case "EDITCOUNTRY":
                    IsCountry = true;
                    Country = (WorkingObjectsRepository.WorkObject as CountryNPC) ?? new();
                    Country.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();
                    break;
                case "ADDCITY":
                case "EDITCITY":
                    IsCity = true;
                    City = (WorkingObjectsRepository.WorkObject as CityNPC) ?? new();
                    Countries = WorkingObjectsRepository.Countries ?? [];
                    Country = Countries.Where(e => e.Id == City.CountryId)
                        .SingleOrDefault() ?? new();
                    City.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();
                    break;
                case "ADDTERMINAL":
                case "EDITTERMINAL":
                    IsTerminal = true;
                    Terminal = (WorkingObjectsRepository.WorkObject as TerminalNPC) ?? new();
                    Airports = WorkingObjectsRepository.Airports ?? [];
                    Airport = Airports.Where(e => e.Id == Terminal.AirportId)
                        .SingleOrDefault() ?? new();
                    Terminal.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();
                    break;
                case "ADDGATE":
                case "EDITGATE":
                    IsGate = true;
                    Gate = (WorkingObjectsRepository.WorkObject as GateNPC) ?? new();
                    Terminals = WorkingObjectsRepository.Terminals ?? [];
                    Terminal = Terminals.Where(e => e.Id == Gate.TerminalId)
                        .SingleOrDefault() ?? new();
                    Gate.AirportId = Terminal.AirportId;
                    Gate.PropertyChanged += (s, e) => ActionCommand.NotifyCanExecuteChanged();
                    break;
                case "ADDSEATTYPE":
                case "EDITSEATTYPE":
                    IsSeatType = true;
                    SeatType = (WorkingObjectsRepository.WorkObject as SeatTypeNPC) ?? new();
                    break;
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            switch (ActionKey)
            {
                case "ADDCOUNTRY":
                    if (AddCommand.Add(Country?.ConvertToCountry() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    {
                        Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                        Country = new();
                    }
                    break;
                case "EDITCOUNTRY":
                    if (EditCommand.Edit(Country?.ConvertToCountry() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                            Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                    break;
                case "ADDCITY":
                    City.CountryId = Country.Id;
                    if (AddCommand.Add(City?.ConvertToCity() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    {
                        Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                        Country = new();
                    }
                    break;
                case "EDITCITY":
                    City.CountryId = Country.Id;
                    if (EditCommand.Edit(City?.ConvertToCity() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                            Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                    break;
                case "ADDTERMINAL":
                    Terminal.AirportId = Airport.Id;
                    if (AddCommand.Add(Terminal?.ConvertToTerminal() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    {
                        Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                        Terminal = new();
                    }
                    break;
                case "EDITTERMINAL":
                    Terminal.AirportId = Airport.Id;
                    if (EditCommand.Edit(Terminal?.ConvertToTerminal() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                            Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                    break;
                case "ADDGATE":
                    Gate.TerminalId = Terminal.Id;
                    Gate.AirportId = Terminal.AirportId;
                    if (AddCommand.Add(Gate?.ConvertToGate() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    {
                        Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                        Gate = new();
                    }
                    break;
                case "EDITGATE":
                    Gate.TerminalId = Terminal.Id;
                    Gate.AirportId = Terminal.AirportId;
                    if (EditCommand.Edit(Gate?.ConvertToGate() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                            Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                    break;
                case "ADDSEATTYPE":
                    if (AddCommand.Add(SeatType?.ConvertToSeatType() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    {
                        Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                        SeatType = new();
                    }
                    break;
                case "EDITSEATTYPE":
                    if (EditCommand.Edit(SeatType?.ConvertToSeatType() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                            Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                    break;
            }
        }

        public bool CanExecuteAction()
        {
            switch (ActionKey)
            {
                case "ADDCOUNTRY":
                case "EDITCOUNTRY":
                    return Country?.Name != "";
                case "ADDCITY":
                case "EDITCITY":
                    return City?.Name != "" &&
                        Country != null;
                case "ADDTERMINAL": 
                case "EDIRCOMMAND":
                    return Terminal?.Name != "" &&
                        Airport != null;
                case "ADDGATE":
                case "EDITGATE":
                    return Gate?.Name != "" &&
                        Terminal != null;
                case "ADDSEATTYPE":
                case "EDITSEATTYPE":
                    return SeatType?.Name != "";
            }
            return false;
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
