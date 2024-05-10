using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace AdminModule.Resources.Models
{
    public partial class GateNPC : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private int terminalId;
        [ObservableProperty]
        private int airportId;

        public static GateNPC ConvertFromGateToNew(Gate gate)
        {

            return new()
            {
                Id = gate.Id,
                Name = gate.Name,
                TerminalId = gate.TerminalId,
                AirportId = gate.AirportId,
            };
        }

        public Gate ConvertToGate()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name ?? "",
                TerminalId = this.TerminalId,
                AirportId = this.AirportId
            };
        }

    }
}
