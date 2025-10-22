﻿using System.ComponentModel;
using System.Diagnostics;
using Presentation.Models;

namespace Presentation.Services
{
    public class TrackedWindowState
    {
        public TrackedWindowState(BaseSettings settings)
        {
            // UpdateIsOpen(settings.IsOpen);
            // UpdateIsInTestMode(settings.IsInTestMode);
            // UpdateIsInDebugMode(false);
        }

        public bool IsOpen { get; private set; }
        public bool IsInTestMode { get; private set; }
        public bool IsInDebugMode { get; private set; }

        public bool RequiresChange { get; private set; }

        public void Update(bool isCarOnTrack)
        {
            if (!IsInTestMode)
            {
                UpdateIsOpen(isCarOnTrack);
            }
        }

        public void Update(PropertyChangedEventArgs eventArgs)
        {
            string propertyName = eventArgs.PropertyName!;

            if (propertyName == nameof(IsOpen))
            {
                UpdateIsOpen(!IsOpen);
            }
            else if (propertyName == nameof(IsInTestMode))
            {
                UpdateIsInTestMode(!IsInTestMode);
            }
            else if (propertyName == nameof(IsInDebugMode))
            {
                UpdateIsInDebugMode(!IsInDebugMode);
            }
        }

        public void CompleteChange()
        {
            RequiresChange = false;
        }

        private void UpdateIsOpen(bool isOpen)
        {
            if (IsOpen == !isOpen)
            {
                IsOpen = isOpen;
                Debug.WriteLine("Raising is Open");
                RaiseChange();
            }
        }

        // private void UpdateIsEnabled(bool isEnabled)
        // {
        //     if (IsEnabled == !isEnabled)
        //     {
        //         IsEnabled = isEnabled;
        //         RaiseChange();
        //     }
        // }

        private void UpdateIsInTestMode(bool isInTestMode)
        {
            if (IsInTestMode == !isInTestMode)
            {
                IsInTestMode = isInTestMode;
                Debug.WriteLine("Raising is Test Mode");
                RaiseChange();
            }
        }

        private void UpdateIsInDebugMode(bool isInDebugMode)
        {
            if (IsInDebugMode == !isInDebugMode)
            {
                IsInDebugMode = isInDebugMode;
                Debug.WriteLine("Raising is Debug Mode");
                RaiseChange();
            }
        }

        private void RaiseChange()
        {
            RequiresChange = true;
        }
    }
}
