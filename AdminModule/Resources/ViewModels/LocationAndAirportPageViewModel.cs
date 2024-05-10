using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
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

        [ObservableProperty]
        private ObservableCollection<CityNPC>? cities;
        [ObservableProperty]
        private CityNPC? city;

        [ObservableProperty]
        private ObservableCollection<AirportNPC>? airports;
        [ObservableProperty]
        private AirportNPC? airport;

        [ObservableProperty]
        private ObservableCollection<GateNPC>? gates;
        [ObservableProperty]
        private GateNPC? gate;

        [ObservableProperty]
        private ObservableCollection<TerminalNPC>? terminals;
        [ObservableProperty]
        private TerminalNPC? terminal;

        public LocationAndAirportPageViewModel()
        {
            IsLocation = true;
            IsAirport = false;

            Countries = WorkingObjectsRepository.Countries;
            Airports = WorkingObjectsRepository.Airports;
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

        }
        [RelayCommand]
        private void Modify(object obj) 
        {

        }
    }
}
