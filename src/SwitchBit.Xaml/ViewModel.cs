using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Xaml
{
    /// <summary>
    /// Base class for all UI related ViewModels (Observable)
    /// </summary>
    public abstract class ViewModel : ObservableObject
    {
        private bool _IsBusy;
        /// <summary>
        /// True if the interface is processing something in the background, otherwise false
        /// (Observable)
        /// </summary>
        public bool IsBusy { get => _IsBusy; set { SetProperty(ref _IsBusy, value); } }

        string _title = string.Empty;
        /// <summary>
        /// Gets or sets the title message for the current view
        /// (Observable)
        /// </summary>
        public string Title { get => _title; set { SetProperty(ref _title, value); } }

        string _statusMessage = string.Empty;
        /// <summary>
        /// Gets or sets the StatusMessage for the current view
        /// (Observable)
        /// </summary>
        public string StatusMessage { get => _statusMessage; set { SetProperty(ref _statusMessage, value); } }        
    }
}
