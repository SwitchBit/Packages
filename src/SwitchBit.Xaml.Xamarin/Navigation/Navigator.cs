using SwitchBit.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwitchBit.Xaml.Xamarin
{
    public static class Navigator
    {
        public static async Task Push(ViewModel viewModel)
        {
            var view = NavigationMap.GetPage(viewModel.GetType());
            view.BindingContext = viewModel;
            await Navigation.PushAsync(view);
        }

        public static async Task Pop()
            => await Navigation.PopAsync();

        public static async Task PushModal(ViewModel viewModel, bool wrapInNavigation = true)
        {
            var view = NavigationMap.GetPage(viewModel.GetType());
            view.BindingContext = viewModel;
            await Navigation.PushModalAsync(wrapInNavigation ? new NavigationPage(view) : view);
        }

        public static async Task PopModal()
            => await Navigation.PopModalAsync();

        public static void SetBase(object viewModel, bool wrapInNavigation = true)
        {
            var vmType = viewModel.GetType();
            var view = NavigationMap.GetPage(vmType);
            view.BindingContext = viewModel;
            Application.Current.MainPage = wrapInNavigation ? new NavigationPage(view) : view;
        }

        static INavigation Navigation
        {   //If the tab page has Navigation controllers as the contents, we need to use those.
            get => (Application.Current.MainPage as TabbedPage)?.CurrentPage?.Navigation ?? Application.Current.MainPage.Navigation;
        }
    }
}
