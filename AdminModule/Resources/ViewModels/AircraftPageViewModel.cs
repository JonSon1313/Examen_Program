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
    public partial class AircraftPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<AircraftNPC>? aircrafts;

        public AircraftPageViewModel()
        {
            Aircrafts = WorkingObjectsRepository.Aircrafts;
        }

        [RelayCommand]
        private void Delete(object obj)
        {
            if(obj is AircraftNPC)
            {
                if(RemoveCommand.Remove(new Aircraft(),(obj as AircraftNPC)!.Id,
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("EndPoint is Missing")))
                {
                    Shell.Current.DisplayAlert("Success", "Object has been successfully removed", "Ok");
                }
            }
        }

        [RelayCommand]
        private async Task Modify(object obj)
        { 
            if(obj is AircraftNPC)
            {
                WorkingObjectsRepository.WorkObject = obj;
                WorkingObjectsRepository.Action = "Edit";
                await Shell.Current.GoToAsync(nameof(AddOrEditAircraftPage));
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            WorkingObjectsRepository.WorkObject = new AircraftNPC()
            {
                Id = -1,
                Manufacturer = "",
                Model = "",
                TailNumber = ""
            };
            WorkingObjectsRepository.Action = "Add";
            await Shell.Current.GoToAsync(nameof(AddOrEditAircraftPage));
        }
    }
}
