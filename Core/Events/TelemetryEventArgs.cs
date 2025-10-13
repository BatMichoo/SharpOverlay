using Core.Models;
using iRacingSdkWrapper;

namespace Core.Events;

public class TelemetryEventArgs : EventArgs
{
    public readonly SimulationOutputDTO SimOutput;

    public TelemetryEventArgs(TelemetryInfo telemetryInfo)
    {
        SimOutput = new SimulationOutputDTO(telemetryInfo);
    }
}
