using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models;

namespace DBLayer
{
    public class FlyCompanyDBInstance
    {
        readonly FlyCompanyDBContext dbContext;

        public FlyCompanyDBInstance()
        {
            FlyCompanyDBContextBuilder builder = new ();
            dbContext = builder.CreateDbContext([]);
        }
        public Salt? GetSalt(string _login)
        {
            return dbContext.Salts.Where(s => s.Login == _login).FirstOrDefault();
        }

        public void AddSalt(Salt _salt)
        {
            dbContext.Salts.Add(new()
            {
                Login = _salt.Login,
                SaltString = _salt.SaltString,
            });
            dbContext.SaveChanges();
        }
        // new code here
        public void AddAdministration(Administrator admin)
        {
            dbContext.Administrators.Add(new Administrator
            {
                FirstName = admin.FirstName,
                MiddleName = admin.MiddleName,
                LastName = admin.LastName,
                Login = admin.Login,
                Password = admin.Password,
                PhoneNumber = admin.PhoneNumber,
                Email = admin.Email,
            });
            dbContext.SaveChanges();
        }

        public Administrator? GetAdministrator(string login, string password)
        {
            return dbContext.Administrators.FirstOrDefault(a => a.Login == login && a.Password == password);
        }

        public void AddAircraft(Aircraft aircraft)
        {
            dbContext.Aircrafts.Add(new Aircraft
            {
                Manufacturer = aircraft.Manufacturer,
                Model = aircraft.Model,
                TailNumber = aircraft.TailNumber,  
            });
        }

        public List<Aircraft>? GetAircraft()
        {
            return dbContext?.Aircrafts.ToList();
        }

        public void AddAirport(Airport airport)
        {
            dbContext.Airports.Add(new Airport
            {
                FullName = airport.FullName,   
                IATACode = airport.IATACode,    
                ICAOCode = airport.ICAOCode,
                CityId = airport.CityId,
                CountryId = airport.CountryId,  
            });
        }

        public List<Airport>? GetAirport()
        {
            return dbContext?.Airports.Include(a => a.Gates).Include(a => a.Terminals).ToList();
        }

        public void AddCity(City city)
        {
            dbContext.Cities.Add(new City
            {
                Name = city.Name,
                CountryId = city.CountryId   
            });
        }

        public List<City>? GetCity() 
        { 
           return dbContext?.Cities.ToList();
        }

        public void AddClient(Client client) 
        {
            dbContext.Clients.Add(new Client
            {
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName, 
                Login = client.Login,
                Password = client.Password,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,   
            });
        }

        public Client? GetClient(string login, string password)
        {
            return dbContext?.Clients.FirstOrDefault(a => a.Login == login && a.Password == password);  
        }

        public void AddCountry(Country country)
        {
            dbContext.Countries.Add(new Country
            {
                Name = country.Name

            });
        }

        public List<Country>? GetCountry()
        {
            return dbContext?.Countries.Include(a => a.Cities).ToList();
        }

        public void AddFligt(Flight fligt)
        {
            dbContext.Flights.Add(new Flight
            {
                Number = fligt.Number,
                Aircraft = fligt.Aircraft,
                DepartureTime = fligt.DepartureTime,
                FromId = fligt.FromId,
                ToId = fligt.ToId,
                BasePrice = fligt.BasePrice,
            });
        }

        public List<Flight>? GetFlights()
        {
            return dbContext?.Flights.Include(a => a.Seats).ToList();
        }

        public void AddGate(Gate gate)
        {
            dbContext.Gates.Add(new Gate
            {
                Name = gate.Name,
                AirportId = gate.AirportId,
                TerminalId = gate.TerminalId,   

            });
        }

        public void AddSeatType(SeatType seatType)
        {
            dbContext.SeatTypes.Add(new SeatType
            {
                Name= seatType.Name,
            });
        }

        public List<SeatType>? GetSeatTypes()
        {
            return dbContext?.SeatTypes.ToList();
        }

        public void AddTerminal (Terminal terminal)
        {
            dbContext.Terminals.Add(new Terminal
            {
                Name = terminal.Name,
                AirportId = terminal.AirportId
            });
        }

        public List<Terminal>? GetTerminals()
        {
            return dbContext?.Terminals.ToList();   
        }


        // nothing special
    }
}
