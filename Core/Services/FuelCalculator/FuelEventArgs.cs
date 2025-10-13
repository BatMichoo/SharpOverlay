using Core.Models;

namespace Core.Services.FuelCalculator
{
    public class FuelEventArgs : EventArgs
    {
        public FuelEventArgs(FuelViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public FuelViewModel ViewModel { get; }
    }
}
