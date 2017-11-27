using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Xaml
{
    /// <summary>
    /// Store and retrieve loaded <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/> instances. Only a single instance per type is allowed to be stored.
    /// Helpful for a quick spike of a UI without a ton of framework. 
    /// </summary>
    public sealed class ViewModelLocator
    {
        private static readonly Lazy<ViewModelLocator> _instance = new Lazy<ViewModelLocator>(() => new ViewModelLocator());

        private readonly Dictionary<Type, ViewModel> _Models = new Dictionary<Type, ViewModel>();

        /// <summary>
        /// Inserts a <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/> instance of the TViewModel type to be saved
        /// </summary>
        /// <typeparam name="TViewModel">Type of the <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/></typeparam>
        /// <param name="viewModel"><see cref="SwitchBit.Xaml.Mvvm.ViewModel"/>instance of the type provided by TViewModel</param>
        public static void AddOrOverwrite<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
            => _instance.Value._Models[typeof(TViewModel)] = viewModel;
        /// <summary>
        /// Returns true if an instance of the type provided for TViewModel exists, otherwise false
        /// </summary>
        /// <typeparam name="TViewModel">Type of the <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/> </typeparam>
        /// <returns>bool</returns>
        public static bool ModelExists<TViewModel>() where TViewModel : ViewModel
            => Resolve<TViewModel>() != null;
        /// <summary>
        /// Returns an instance of the requested viewmodel if it exists, otherwise null
        /// </summary>
        /// <typeparam name="TViewModel">Type of the <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/> to retrieve</typeparam>
        /// <returns>Instance of <see cref="SwitchBit.Xaml.Mvvm.ViewModel"/></returns>
        public static TViewModel Resolve<TViewModel>() where TViewModel : ViewModel
            => (_instance.Value._Models.TryGetValue(typeof(TViewModel), out ViewModel viewModel)) ? (TViewModel)viewModel : null;
    }
}