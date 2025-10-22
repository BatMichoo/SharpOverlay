using Core.Events;

namespace Core.Services.FuelCalculator
{
    public interface IFuelService : IDisposable
    {
        SimReader SimReader { get; }

        event EventHandler<FuelEventArgs> FuelUpdated;
    }
}
