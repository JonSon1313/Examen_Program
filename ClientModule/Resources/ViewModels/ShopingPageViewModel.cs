using CommunityToolkit.Mvvm.ComponentModel;
using ClientModule.Resources.Models;
using System.Collections.ObjectModel;
using Models;
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
            Departure_country = Countries[0];
            Departure_countryId = 0;
            Arrival_country = Countries[0];
            Arrival_countryId = 0;
            Departure_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities.Where(c => c.CountryId == departure_country.Id).ToList());
            Arrival_cities = new ObservableCollection<CityNPC>(WorkingObjectsRepository.Cities.Where(c => c.CountryId == arrival_country.Id).ToList());
            Departure_city = WorkingObjectsRepository.Cities.Where(c => c.CountryId == departure_country.Id).SingleOrDefault();
            Arrival_city = WorkingObjectsRepository.Cities.Where(c => c.CountryId == arrival_country.Id).SingleOrDefault();
            Flights = new ObservableCollection<FlightNPC> (WorkingObjectsRepository.Flights.Where(c => c.ArrivaleCity == arrival_city.Name && c.DepartureCity == departure_city.Name).ToList());
        }
        [RelayCommand]
        private void DepartureCountryChanged()
        {

            var departure_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CountryId == departure_country.Id).Select(c => c.Id).ToList();
            var temp = new List<int>();
            if (Departure_country != null)
            {
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (departure_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }
                }

            }
            else if (Departure_country != null && Arrival_country != null)
            {
                var arrival_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CountryId == arrival_country.Id).Select(c => c.Id).ToList();
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (departure_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId) && arrival_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }

                }
            }
            Flights = new ObservableCollection<FlightNPC>();

            for (int i = 0; i < WorkingObjectsRepository.Flights.Count; i++)
            {
                if (temp.Contains(WorkingObjectsRepository.Flights[i].FromId))
                {
                    Flights.Add(WorkingObjectsRepository.Flights[i]);
                }
            }

        }

        [RelayCommand]
        private void DepartureCityChanged()
        {
            var departure_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CityId == departure_city.Id).Select(c => c.Id).ToList();
            var temp = new List<int>();
            if (Departure_city != null)
            {
                for(int i =0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if ( departure_airports_tempId.Contains (WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }
                }
                
            }
            else if(Departure_city != null && Arrival_city != null)
            {
                var arrival_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CityId == arrival_city.Id).Select(c=>c.Id).ToList();
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (departure_airports_tempId.Contains (WorkingObjectsRepository.Gates[i].AirportId) && arrival_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }

                }
            }
            Flights = new ObservableCollection<FlightNPC>();

            for (int i = 0; i < WorkingObjectsRepository.Flights.Count; i++)
            {
                if (temp.Contains(WorkingObjectsRepository.Flights[i].FromId))
                {
                    Flights.Add(WorkingObjectsRepository.Flights[i]);
                }
            }

        }


        [RelayCommand]
        private void ArrivalCountryChanged()
        {
            var arrival_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CountryId == arrival_country.Id).Select(c => c.Id).ToList();
            var temp = new List<int>();
            if (Arrival_country != null)
            {
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (arrival_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }
                }

            }
            else if (Departure_country != null && Arrival_country != null)
            {
                var departure_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CountryId == departure_country.Id).Select(c => c.Id).ToList();
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (departure_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId) && arrival_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }

                }
            }
            Flights = new ObservableCollection<FlightNPC>();

            for (int i = 0; i < WorkingObjectsRepository.Flights.Count; i++)
            {
                if (temp.Contains(WorkingObjectsRepository.Flights[i].FromId))
                {
                    Flights.Add(WorkingObjectsRepository.Flights[i]);
                }
            }
        }

        [RelayCommand]
        private void ArrivalCityChanged()
        {
            var arrival_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CityId == arrival_city.Id).Select(c => c.Id).ToList();
            var temp = new List<int>();
            if (Arrival_city != null)
            {
                for(int i =0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if ( arrival_airports_tempId.Contains (WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }
                }
                
            }
            else if(Departure_city != null && Arrival_city != null)
            {
                var departure_airports_tempId = WorkingObjectsRepository.Airports.Where(c => c.CityId == departure_city.Id).Select(c=>c.Id).ToList();
                for (int i = 0; i < WorkingObjectsRepository.Gates.Count; i++)
                {
                    if (departure_airports_tempId.Contains (WorkingObjectsRepository.Gates[i].AirportId) && arrival_airports_tempId.Contains(WorkingObjectsRepository.Gates[i].AirportId))
                    {
                        temp.Add(WorkingObjectsRepository.Gates[i].AirportId);
                    }

                }
            }
            Flights = new ObservableCollection<FlightNPC>();

            for (int i = 0; i < WorkingObjectsRepository.Flights.Count; i++)
            {
                if (temp.Contains(WorkingObjectsRepository.Flights[i].FromId))
                {
                    Flights.Add(WorkingObjectsRepository.Flights[i]);
                }
            }
        }

        [RelayCommand]
        private async void OpenOrderPage(object obj)
        {
            WorkingObjectsRepository.TargetFlight = obj as FlightNPC;
            await Shell.Current.GoToAsync($"{nameof(OrderPage)}");
        }
    }
}

