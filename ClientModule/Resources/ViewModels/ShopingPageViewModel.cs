using CommunityToolkit.Mvvm.ComponentModel;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ClientModule.Resources.Repositories;
using ClientModule.Resources.Views;

namespace ClientModule.Resources.ViewModels
{
    public partial class ShopingPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<TicketNPC>? tickets;
        [ObservableProperty]
        ObservableCollection<CountryNPC>? countries;
        [ObservableProperty]
        ObservableCollection<FlightNPC>? flights;
        [ObservableProperty]
        FlightNPC? flight;
        [ObservableProperty]
        CountryNPC? departure_country;
        [ObservableProperty]
        CountryNPC? arrival_country;
        [ObservableProperty]
        CityNPC? departure_city;
        [ObservableProperty]
        CityNPC? arrival_city;
        [ObservableProperty]
        ObservableCollection<CityNPC>? arrival_cities;
        [ObservableProperty]
        ObservableCollection<CityNPC>? departure_cities;
        [ObservableProperty]
        DateTime? flightDate;
        [ObservableProperty]
        int departure_countryId;
        [ObservableProperty]
        int arrival_countryId;
        [ObservableProperty]
        int departure_cityId;
        [ObservableProperty]
        int arrival_cityId;
        public ShopingPageViewModel()
        {
            Countries = WorkingObjectsRepository.Countries;
            
            Departure_country = Countries?[0] ?? new();
            Departure_countryId = 0;

            Arrival_country = Countries?[0] ?? new();
            Arrival_countryId = 0;

            Departure_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == departure_country?.Id).ToList());
            Arrival_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == arrival_country?.Id).ToList());
            Departure_city = WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == departure_country?.Id).FirstOrDefault();
            Arrival_city = WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == arrival_country?.Id).FirstOrDefault();
            Flights = new ObservableCollection<FlightNPC> (WorkingObjectsRepository.Flights!
                .Where(c => c.ArrivaleCity == arrival_city?.Name && c.DepartureCity == departure_city?.Name).ToList());
        }
        [RelayCommand]
        private void DepartureCountryChanged()
        {

            if (Departure_country?.Name != "" && Arrival_country?.Name != "")
            {
                Flights = new ObservableCollection<FlightNPC>(WorkingObjectsRepository.Flights?
                    .Where(c => c.ArrivaleCountry == Arrival_country?.Name && c.DepartureCountry == Departure_country?.Name).ToList() ?? []);
            }

            Departure_city = WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == departure_country?.Id).FirstOrDefault();
            Departure_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == Departure_country?.Id).ToList());
        }

        [RelayCommand]
        private void DepartureCityChanged()
        {
            if (Departure_city?.Name != "" && Arrival_city?.Name != "")
            {
                Flights = new ObservableCollection<FlightNPC>(WorkingObjectsRepository.Flights?
                .Where(c => c.ArrivaleCity == Arrival_city?.Name && c.DepartureCity == Departure_city?.Name).ToList() ?? []);
            }
        }

        [RelayCommand]
        private void ArrivalCountryChanged()
        {
            if (Departure_country?.Name != "" && Arrival_country?.Name != "")
            {
                Flights = new ObservableCollection<FlightNPC>(WorkingObjectsRepository.Flights?
                .Where(c => c.ArrivaleCountry == Arrival_country?.Name && c.DepartureCountry == Departure_country?.Name).ToList() ?? []);
            }

            Arrival_city = WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == Arrival_country?.Id).FirstOrDefault();
            Arrival_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities!
                .Where(c => c.CountryId == Arrival_country?.Id).ToList());
        }

        [RelayCommand]
        private void ArrivalCityChanged()
        {
            if (Departure_city?.Name != "" && Arrival_city?.Name != "")
            {
                Flights = new ObservableCollection<FlightNPC>(WorkingObjectsRepository.Flights?
                .Where(c => c.ArrivaleCity == Arrival_city?.Name && c.DepartureCity == Departure_city?.Name).ToList() ?? []);
            }

        }

        [RelayCommand]
        private async Task OpenOrderPage(object obj)
        {
            WorkingObjectsRepository.TargetFlight = obj as FlightNPC;
            await Shell.Current.GoToAsync($"{nameof(OrderPage)}");
        }
    }
}