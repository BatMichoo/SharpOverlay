﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Core.Services.FuelCalculator;
using Presentation.Events;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Overlays
{
    /// <summary>
    /// Interaction logic for FuelCalculator.xaml
    /// </summary>
    public partial class FuelCalculatorWindow : Window
    {
        private readonly IFuelService _fuelService;
        private readonly WindowStateService _windowStateService;

        private readonly FuelSettings _settings = App.appSettings.FuelSettings;

        private FuelDebugWindow? _fuelDebugWindow;

        public FuelCalculatorWindow()
        {
            _fuelService = new FuelCalculatorService();
            _windowStateService = new WindowStateService(_fuelService.SimReader, _settings);

            _fuelService.FuelUpdated += OnFuelUpdate;
            _windowStateService.WindowStateChanged += OnWindowStateChange;

            JotService.tracker.Track(this);

            Topmost = true;

            InitializeComponent();
        }

        private void OnWindowStateChange(object? sender, WindowStateEventArgs e)
        {
            if ((e.IsOpen || e.IsInTestMode) && e.IsEnabled)
            {
                Show();
            }
            else
            {
                Hide();
            }

            if (e.IsInDebugMode)
            {
                _fuelDebugWindow = new FuelDebugWindow(_fuelService);
                _fuelDebugWindow.Show();
            }
            else if (_fuelDebugWindow is not null && !e.IsInDebugMode)
            {
                _fuelService.FuelUpdated -= _fuelDebugWindow!.ExecuteOnFuelUpdated;
                _fuelDebugWindow.Hide();
                _fuelDebugWindow = null;
            }
        }

        private void Window_MouseDown(object? sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void OnFuelUpdate(object? sender, FuelEventArgs e)
        {
            this.DataContext = e.ViewModel;
        }

        private void OnPropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if (_fuelDebugWindow is not null)
            {
                _fuelService.FuelUpdated -= _fuelDebugWindow!.ExecuteOnFuelUpdated;
                _fuelDebugWindow.Hide();
                _fuelDebugWindow = null;
            }
        }
    }
}
