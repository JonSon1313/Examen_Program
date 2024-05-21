using AdminModule.Resources.Methods;
using AdminModule.Resources.Models;
using AdminModule.Resources.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdminModule.Resources.ViewModels
{
    public partial class AddOrEditAircraftPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ActionCommand))]
        private AircraftNPC? aircraft;
        [ObservableProperty]
        private string actionKey;

        public AddOrEditAircraftPageViewModel()
        {
            Aircraft = WorkingObjectsRepository.WorkObject as AircraftNPC;
            ActionKey = WorkingObjectsRepository.Action;

            if (Aircraft != null)
            {
                Aircraft.PropertyChanged += (s, e) =>
                {
                    ActionCommand.NotifyCanExecuteChanged();
                };
            }
        }

        [RelayCommand (CanExecute = nameof(CanExecuteAction))]
        private void Action()
        {
            if (ActionKey == "Add")
            {
                if (AddCommand.Add(Aircraft?.ConvertToAircraft() ?? new(),
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("EndPoint is Missing")))
                {
                    Shell.Current.DisplayAlert("Success", "Object successfully added", "Ok");
                    Aircraft = new()
                    {
                        Manufacturer = "",
                        Model = "",
                        TailNumber = ""
                    };
                }
            }
            else if (ActionKey == "Edit")
            {
                if (EditCommand.Edit(Aircraft?.ConvertToAircraft() ?? new(),
                    ConnectionCredentialsRepository.EP ??
                    throw new Exception("EndPoint is Missing")))
                {
                    Shell.Current.DisplayAlert("Success", "Object successfully edited", "Ok");
                }
            }
        }
        public bool CanExecuteAction()
        {
            return Aircraft?.Manufacturer != "" &&
                   Aircraft?.Manufacturer.Length <= 50 &&
                   Aircraft?.Model != "" &&
                   Aircraft?.Model.Length <= 100 &&
                   Aircraft?.TailNumber != "" &&
                   Aircraft?.TailNumber.Length <= 10;
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
