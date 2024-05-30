using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class SeatTypeNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;

        public static SeatTypeNPC ConvertFromSeatTypeToNew(SeatType type)
        {

            return new()
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        public SeatType ConvertToSeatType()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? "",
            };
        }
    }
}