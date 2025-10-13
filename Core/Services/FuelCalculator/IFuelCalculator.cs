namespace Core.Services.FuelCalculator
{
    public interface IFuelService
    {
        SimReader SimReader { get; }

        event EventHandler<FuelEventArgs> FuelUpdated;
    }
}
