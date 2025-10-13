using Core.Models;
using iRacingSdkWrapper;
using iRacingSdkWrapper.Bitfields;

namespace Core.Utilities.Telemetries
{
    public interface ITelemetryParser
    {
        public int PlayerCarIdx { get; }
        public int PlayerCarClassId { get; }
        public double PlayerPctOnTrack { get; }
        Dictionary<int, int> PositionCarIdxInClass { get; }
        Dictionary<int, int> PositionCarIdxInRace { get; }
        Dictionary<int, int> CarIdxLastPitLap { get; }
        int CurrentSessionNumber { get; }
        bool HasSwitchedSessions { get; }
        float[] CarIdxPctOnTrack { get; }

        void Clear();
        void ParseCurrentSessionNumber(SimulationOutputDTO telemetry);
        void ParsePlayerCarClassId(SimulationOutputDTO telemetry);
        void ParsePlayerCarIdx(SimulationOutputDTO telemetry);
        void ParsePlayerPctOnTrack(SimulationOutputDTO telemetry);
        void ParsePositionCarIdxInPlayerClass(SimulationOutputDTO telemetry, int paceCarIdx);
        void ParsePositionCarIdxForWholeRace(SimulationOutputDTO telemetry, int paceCarIdx);
        SessionFlags GetSessionFlag(SimulationOutputDTO telemetry);
        void ParseCarIdxOnTrack(SimulationOutputDTO telemetry);
        void ParseCarIdxLastPitLap(SimulationOutputDTO telemetry, int paceCarIdx);
    }
}
