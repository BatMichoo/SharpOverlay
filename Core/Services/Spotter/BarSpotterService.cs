using System.Diagnostics;
using Core.Events;
using Core.Models;
using iRacingSdkWrapper;

namespace Core.Services.Spotter
{
    public class BarSpotterService : IClear
    {
        private const int _carLengthInM = 5;
        private const int _outOfFrameOffset = 1;
        private readonly List<Driver> _drivers = [];
        private Driver _me = new();
        private double _trackLengthInM;
        private Driver? _closest;
        private double _offset = _outOfFrameOffset;
        public SimReader SimReader { get; }

        public BarSpotterService()
        {
            SimReader = new SimReader();
            SimReader.OnTelemetryUpdated += OnTelemetry;
            SimReader.OnSessionUpdated += OnSession;
            SimReader.OnDisconnected += OnDisconnect;
        }

        public event EventHandler<BarSpotterEventArgs>? OnBarUpdated;

        public void OnDisconnect(object? sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            _drivers.Clear();
            _me = new Driver();
            _trackLengthInM = 0;
            _closest = null;
            _offset = _outOfFrameOffset;
        }

        private void OnSession(object? sender, SdkWrapper.SessionUpdatedEventArgs e)
        {
            if (_trackLengthInM == 0)
            {
                _trackLengthInM = e.SessionInfo.WeekendInfo.TrackLength * 1000;
            }

            ParseDrivers(e);
        }

        private void ParseDrivers(SdkWrapper.SessionUpdatedEventArgs e)
        {
            _drivers.Clear();

            foreach (var racer in e.SessionInfo.Drivers)
            {
                var driver = new Driver()
                {
                    CarIdx = racer.CarIdx,
                };

                if (driver.CarIdx == e.SessionInfo.Player.DriverCarIdx)
                {
                    _me = driver;
                }
                else if (driver.CarIdx != e.SessionInfo.Player.PaceCarIdx)
                {
                    _drivers.Add(driver);
                }
            }
        }

        private void OnTelemetry(object? sender, TelemetryEventArgs e)
        {
            var driverTrackPct = e.SimOutput.CarIdxTrackDistPct;

            CalculateRelativeDistanceForAllDrivers(driverTrackPct);

            _closest = FindClosest();

            var distancePerPercentOfTrack = _trackLengthInM / 100;

            _offset = CalculateOffset(_closest.RelativeLapDistancePct, distancePerPercentOfTrack);

            var centeredOffset = GetOffsetInPercentage();

            OnBarUpdated?.Invoke(this, new BarSpotterEventArgs(centeredOffset, e.SimOutput.CarLeftRight));

        }

        private double CalculateOffset(float closestRelativePct, double distancePerPercentOfTrack)
        {
            var distanceToClosestInM = closestRelativePct * distancePerPercentOfTrack;
            var absoluteDistanceToClosest = Math.Abs(distanceToClosestInM);

            if (absoluteDistanceToClosest <= _carLengthInM)
            {
                return distanceToClosestInM / _carLengthInM;
            }

            return _outOfFrameOffset;
        }

        private Driver FindClosest()
        {
            var closest = _drivers.MinBy(d => Math.Abs(d.RelativeLapDistancePct));

            return closest ?? new Driver()
            {
                RelativeLapDistancePct = 2
            };
        }

        private void CalculateRelativeDistanceForAllDrivers(float[] driverTrackPct)
        {
            _me.LapDistancePct = driverTrackPct[_me.CarIdx];

            foreach (var driver in _drivers)
            {
                driver.LapDistancePct = driverTrackPct[driver.CarIdx];
                driver.RelativeLapDistancePct = driver.LapDistancePct - _me.LapDistancePct;
            }
        }

        public double GetOffsetInPercentage()
        {
            return _offset * 100;
        }
    }
}
