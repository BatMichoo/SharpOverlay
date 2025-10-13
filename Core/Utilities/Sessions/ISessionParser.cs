﻿using Core.Models;
using iRacingSdkWrapper;
using iRacingSdkWrapper.JsonModels;

namespace Core.Utilities.Sessions
{
    public interface ISessionParser
    {
        Dictionary<int, Racer> Drivers { get; }
        string EventType { get; }
        SessionType SessionType { get; }
        List<Session> Sessions { get; }
        StartType StartType { get; }
        int SessionLaps { get; }
        int PaceCarIdx { get; }
        bool IsMultiClassRace { get; }
        List<Sector> Sectors { get; }
        int CarId { get; }
        int TrackId { get; }

        void Clear();
        TimeSpan GetBestLapTime(int leaderIdx, int currentSessionNumber);
        void ParseCurrentSessionType(SessionInfo sessionInfo, int currentSessionNumber);
        void ParseRaceType(SessionInfo sessionInfo);
        void ParseDrivers(SessionInfo sessionInfo);
        void ParsePaceCarIdx(SessionInfo sessionInfo);
        void ParseLapsInSession(SessionInfo sessionInfo, int currentSessionNumber);
        void ParseSessions(SessionInfo sessionInfo);
        void ParseStartType(SessionInfo sessionInfo);
        void ParseSectors(SessionInfo sessionInfo);
        void ParseTrackId(SessionInfo sessionInfo);
        void ParseCarId(SessionInfo sessionInfo);
        void ParseEventType(SessionInfo sessionInfo);
    }
}
