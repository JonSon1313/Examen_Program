using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminModule.Resources.ViewModels
{
    public partial class AddOrEditAirportPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CityNPC>? cities;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
        private CityNPC? city;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
        private AirportNPC? airport;

        [ObservableProperty]
        private string? actionKey;

        public AddOrEditAirportPageViewModel()
        {
            ActionKey = WorkingObjectsRepository.Action;
            Cities = WorkingObjectsRepository.Cities;
            Airport = WorkingObjectsRepository.WorkObject as AirportNPC;

            if (ActionKey == "ADD")
                City = new();
            else if (ActionKey == "EDIT")
                City = Cities?.Where(e=>e.Id == Airport?.CityId).FirstOrDefault();

            Airport!.PropertyChanged += (s, e) =>
            {
                ActionCommand.NotifyCanExecuteChanged();
            };
            City!.PropertyChanged += (s, e) =>
            {
                ActionCommand.NotifyCanExecuteChanged();
            };
        }

        [RelayCommand (CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            if(ActionKey == "ADD")
            {
                Airport.CityId = City.Id;
                Airport.CountryId = City.CountryId;
                if(AddCommand.Add(Airport.ConvertToAirport() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing")))
                    Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
            }
            else if (ActionKey == "EDIT")
            {
                Airport.CityId = City.Id;
                Airport.CountryId = City.CountryId;
                EditCommand.Edit(Airport.ConvertToAirport() ?? new(),
                        ConnectionCredentialsRepository.EP ??
                        throw new Exception("EndPoint is Missing"));
            }
        }
        private bool CanExecuteAction()
        {
            return Airport?.FullName != "" &&
                   Airport?.ICAOCode != "" &&
                   Airport?.IATACode != "" &&
                   City?.Name != "";
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
