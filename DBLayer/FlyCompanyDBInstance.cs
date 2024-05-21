
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
        public Administrator AddAdministration(Administrator admin)
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

            return dbContext.Administrators.Where(i => i.FirstName == admin.FirstName).FirstOrDefault() ?? new();
        }

        public Administrator? GetAdministrator(string login, string password)
        {
            return dbContext.Administrators.FirstOrDefault(a => a.Login == login && a.Password == password);
        }
        public void EditAdministration(Administrator admin)
        {
            Administrator? temp = dbContext.Administrators.Where(i => i.Id == admin.Id).FirstOrDefault();
            temp.FirstName = admin.FirstName;
            temp.MiddleName = admin.MiddleName;
            temp.LastName = admin.LastName;
            temp.PhoneNumber = admin.PhoneNumber;
            temp.Email = admin.Email;

            dbContext.Administrators.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteAdministration(int Id)
        {
            dbContext.Administrators?.Remove(dbContext.Administrators.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Aircraft AddAircraft(Aircraft aircraft)
        {
            dbContext.Aircrafts.Add(new Aircraft
            {
                Manufacturer = aircraft.Manufacturer,
                Model = aircraft.Model,
                TailNumber = aircraft.TailNumber,
            });
            dbContext.SaveChanges();

            return dbContext.Aircrafts.Where(i => i.TailNumber == aircraft.TailNumber).FirstOrDefault() ?? new();
        }

        public List<Aircraft>? GetAircraft()
        {
            return dbContext?.Aircrafts.ToList();
        }
        public void EditAircraft(Aircraft aircraft)
        {
            Aircraft? temp = dbContext.Aircrafts.Where(i => i.Id == aircraft.Id).FirstOrDefault();
            temp.Manufacturer = aircraft.Manufacturer;
            temp.Model = aircraft.Model;
            temp.TailNumber = aircraft.TailNumber;

            dbContext.Aircrafts.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteAircraft(int Id)
        {
            dbContext.Aircrafts?.Remove(dbContext.Aircrafts.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();

        }
        public Airport AddAirport(Airport airport)
        {
            dbContext.Airports.Add(new Airport
            {
                FullName = airport.FullName,
                IATACode = airport.IATACode,
                ICAOCode = airport.ICAOCode,
                CityId = airport.CityId,
                CountryId = airport.CountryId,
            });
            dbContext.SaveChanges();

            return dbContext.Airports.Where(i => i.FullName == airport.FullName).FirstOrDefault() ?? new();
        }

        public List<Airport>? GetAirport()
        {
            return dbContext?.Airports.Include(a => a.Gates).Include(a => a.Terminals).ToList();
        }
        public void EditAirport(Airport airport)
        {
            Airport? temp = dbContext.Airports.Where(i => i.Id == airport.Id).FirstOrDefault();
            temp.FullName = airport.FullName;
            temp.IATACode = airport.IATACode;
            temp.ICAOCode = airport.ICAOCode;
            temp.CityId = airport.CityId;
            temp.CountryId = airport.CountryId;

            dbContext.Airports.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteAirport(int Id)
        {
            dbContext.Airports?.Remove(dbContext.Airports.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();

        }
        public City AddCity(City city)
        {
            dbContext.Cities.Add(new City
            {
                Name = city.Name,
                CountryId = city.CountryId
            });
            dbContext.SaveChanges();

            return dbContext?.Cities.Where(i => i.Name == city.Name).FirstOrDefault() ?? new();
        }

        public List<City>? GetCity()
        {
            return dbContext?.Cities.ToList();
        }
        public void EditСity(City city)
        {
            City? temp = dbContext.Cities.Where(i => i.Id == city.Id).FirstOrDefault();
            temp.Name = city.Name;
            temp.CountryId = city.CountryId;

            dbContext.Cities.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteCity(int Id)
        {
            dbContext.Cities?.Remove(dbContext.Cities.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Client AddClient(Client client)
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
            dbContext.SaveChanges();

            return dbContext?.Clients.Where(i => i.FirstName == client.FirstName).FirstOrDefault() ?? new();

        }

        public Client? GetClient(string login, string password)
        {
            return dbContext?.Clients.FirstOrDefault(a => a.Login == login && a.Password == password);
        }

        public void EditClient(Client client)
        {
            Client? temp = dbContext.Clients.Where(i => i.Id == client.Id).FirstOrDefault();
            temp.FirstName = client.FirstName;
            temp.MiddleName = client.MiddleName;
            temp.LastName = client.LastName;
            temp.Login = client.Login;
            temp.Password = client.Password;
            temp.PhoneNumber = client.PhoneNumber;
            temp.Email = client.Email;


            dbContext.Clients.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteClient(int Id)
        {
            dbContext.Clients?.Remove(dbContext.Clients.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Country AddCountry(Country country)
        {
            dbContext.Countries.Add(new Country
            {
                Name = country.Name

            });
            dbContext.SaveChanges();

            return dbContext?.Countries.Where(i => i.Name == country.Name).FirstOrDefault() ?? new();

        }

        public List<Country>? GetCountry()
        {
            return dbContext?.Countries.Include(a => a.Cities).ToList();
        }

        public void EditCountry(Country country)
        {
            Country? temp = dbContext.Countries.Where(i => i.Id == country.Id).FirstOrDefault();
            temp.Name = country.Name;

            dbContext.Countries.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteCountry(int Id)
        {
            dbContext.Countries?.Remove(dbContext.Countries.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Flight AddFlight(Flight flight)
        {
            dbContext.Flights.Add(new Flight
            {
                Number = flight.Number,
                AircraftId = flight.AircraftId,
                DepartureTime = flight.DepartureTime,
                FromId = flight.FromId,
                ToId = flight.ToId,
                BasePrice = flight.BasePrice
            });
            dbContext.SaveChanges();

            return dbContext?.Flights.Where(i => i.Number == flight.Number).FirstOrDefault() ?? new();
        }

        public List<Flight>? GetFlight()
        {
            return dbContext?.Flights.Include(a => a.Seats).ToList();
        }
        public void EditFlight(Flight flight)
        {
            Flight? temp = dbContext.Flights.Where(i => i.Id == flight.Id).FirstOrDefault();
            temp.Number = flight.Number;
            temp.Aircraft = flight.Aircraft;
            temp.DepartureTime = flight.DepartureTime;
            temp.FromId = flight.FromId;
            temp.ToId = flight.ToId;
            temp.BasePrice = flight.BasePrice;

            dbContext.Flights.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteFlight(int Id)
        {
            dbContext.Flights?.Remove(dbContext.Flights.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Gate AddGate(Gate gate)
        {
            dbContext.Gates.Add(new Gate
            {
                Name = gate.Name,
                AirportId = gate.AirportId,
                TerminalId = gate.TerminalId,

            });
            dbContext.SaveChanges();

            return dbContext?.Gates.Where(i => i.Name == i.Name).FirstOrDefault() ?? new();
        }

        public List<Gate>? GetGate()
        {
            return dbContext?.Gates.ToList();
        }
        public void EditGate(Gate gate)
        {
            Gate? temp = dbContext.Gates.Where(i => i.Id == gate.Id).FirstOrDefault();
            temp.Name = gate.Name;
            temp.AirportId = gate.AirportId;
            temp.TerminalId = gate.TerminalId;

            dbContext.Gates.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteGate(int Id)
        {
            dbContext.Gates?.Remove(dbContext.Gates.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Salt AddSalt(Salt _salt)
        {
            dbContext.Salts.Add(new()
            {
                Login = _salt.Login,
                SaltString = _salt.SaltString,
            });
            dbContext.SaveChanges();

            return dbContext.Salts.Where(i => i.Login == _salt.Login).FirstOrDefault() ?? new();
        }
        public Salt? GetSalt(string _login)
        {
            return dbContext.Salts.Where(s => s.Login == _login).FirstOrDefault();
        }
        // new code here
        public SeatType AddSeatType(SeatType seatType)
        {
            dbContext.SeatTypes.Add(new SeatType
            {
                Name = seatType.Name
            });
            dbContext.SaveChanges();

            return dbContext?.SeatTypes.Where(i => i.Name == seatType.Name).FirstOrDefault() ?? new();
        }

        public List<SeatType>? GetSeatType()
        {
            return dbContext?.SeatTypes.ToList();
        }
        public void EditSeatType(SeatType seatType)
        {
            SeatType? temp = dbContext.SeatTypes.Where(i => i.Id == seatType.Id).FirstOrDefault();
            temp.Name = seatType.Name;

            dbContext.SeatTypes.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteSeatType(int Id)
        {
            dbContext.SeatTypes?.Remove(dbContext.SeatTypes.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
        public Terminal AddTerminal(Terminal terminal)
        {
            dbContext.Terminals.Add(new Terminal
            {
                Name = terminal.Name,
                AirportId = terminal.AirportId
            });
            dbContext.SaveChanges();

            return dbContext?.Terminals.Where(i => i.Name == terminal.Name).FirstOrDefault() ?? new();
        }

        public List<Terminal>? GetTerminal()
        {
            return dbContext?.Terminals.ToList();
        }
        public void EditTerminal(Terminal terminal)
        {
            Terminal? temp = dbContext.Terminals.Where(i => i.Id == terminal.Id).FirstOrDefault();
            temp.Name = terminal.Name;

            dbContext.Terminals.Update(temp);
            dbContext.SaveChanges();
        }

        public void DeleteTerminal(int Id)
        {
            dbContext.Terminals?.Remove(dbContext.Terminals.Where(i => i.Id == Id).FirstOrDefault() ?? new());
            dbContext.SaveChanges();
        }
    }
}
