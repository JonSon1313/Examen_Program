using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ClientModule.Resources.Models
{
    public partial class TerminalNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        public int airportId;


        public static TerminalNPC ConvertFromTerminalToNew(Terminal terminal)
        {

            return new()
            {
                Id = terminal.Id,
                Name = terminal.Name,
                AirportId = terminal.AirportId,
            };
        }

        public Terminal ConvertToTerminal()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? "",
                AirportId = this.AirportId,
            };
        }

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}
