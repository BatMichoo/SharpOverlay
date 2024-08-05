﻿using Jot.Configuration;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Colors = System.Windows.Media.Colors;

namespace SharpOverlay.Models
{
    public class BarSpotterSettings : INotifyPropertyChanged
    {
        private bool _isEnabled;

        private double _barWidth;

        private double _barLength;

        private SolidColorBrush? _barColor;

        private SolidColorBrush? _threeWideBarColor;

        private bool _testMode;
        public bool TestMode
        {
            get => _testMode;
            set
            {
                _testMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestMode)));
            }
        }

        public bool IsEnabled 
        {
            get => _isEnabled; 
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }
        public double BarWidth
        {
            get => _barWidth;
            set
            {
                _barWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BarWidth)));
            }
        }

        public double BarLength
        {
            get => _barLength;
            set
            {
                _barLength = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BarLength)));   
            }
        }

        public SolidColorBrush? BarColor
        {
            get => _barColor;
            set
            {
                _barColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BarColor)));
            }
        }

        public SolidColorBrush? ThreeWideBarColor
        {
            get => _threeWideBarColor;
            set
            {
                _threeWideBarColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThreeWideBarColor)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class InputGraphSettings : INotifyPropertyChanged
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        private bool _showClutch;
        public bool ShowClutch 
        { 
         get => _showClutch;
            set
            {
                _showClutch = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowClutch)));
            }
        }


        private bool _useRawValues;
        public bool UseRawValues 
        {
            get => _useRawValues;
            set
            {
                _useRawValues = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UseRawValues)));
            }
        }
        private SolidColorBrush? _throttleColor;

        private SolidColorBrush? _brakeColor;

        private SolidColorBrush? _clutchColor;
        public SolidColorBrush? ThrottleColor
        {
            get => _throttleColor;
            set
            {
                _throttleColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThrottleColor)));
            }
        }

        public SolidColorBrush? BrakeColor
        {
            get => _brakeColor;
            set
            {
                _brakeColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrakeColor)));
            }
        }

        public SolidColorBrush? ClutchColor
        {
            get => _clutchColor;
            set
            {
                _clutchColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClutchColor)));
            }
        }

        private int _lineWidth;

        public int LineWidth
        {
            get => _lineWidth;
            set
            {
                _lineWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LineWidth)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public InputGraphSettings()
        {
            ThrottleColor = new SolidColorBrush(Colors.Green);
            BrakeColor = new SolidColorBrush(Colors.Red);
            ClutchColor = new SolidColorBrush(Colors.Blue);
            UseRawValues = true;
            ShowClutch = true;
        }
    }

    public class Settings : INotifyPropertyChanged
    {
        public BarSpotterSettings BarSpotterSettings { get; set; }
        public InputGraphSettings InputGraphSettings { get; set; }

        public bool IsUpdate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Settings()
        {
            InputGraphSettings = new InputGraphSettings();
            BarSpotterSettings = new BarSpotterSettings();
        }
    }
}
