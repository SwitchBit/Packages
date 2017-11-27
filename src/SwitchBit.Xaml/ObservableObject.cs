using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

//NOTE: Stole this code fair and square from James Montemagno, no sense reinventing
// https://github.com/jamesmontemagno/mvvm-helpers/blob/master/MvvmHelpers

namespace SwitchBit.Xaml
{
    /// <summary>
    /// Object implementation of INotifyPropertyChanged interface, providing PropertyChanged event
    /// and a SetProperty method
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}