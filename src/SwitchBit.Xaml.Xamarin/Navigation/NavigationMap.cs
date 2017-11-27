using SwitchBit.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SwitchBit.Xaml.Xamarin
{
    /// <summary>
    /// Controls UI movement within a Xamarin Forms application using viewmodel-based navigation
    /// </summary>
    public static class NavigationMap
    {
        static readonly Dictionary<Type, Type> RegisteredTypes = new Dictionary<Type, Type>();

        static readonly Dictionary<Type, object> Singletons = new Dictionary<Type, object>();

        public static void Register<TViewModel, TPage>() where TPage : TViewModel
            => RegisteredTypes[typeof(TViewModel)] = typeof(TPage);
        public static void Register(Type viewModelType, Type pageType)
            => RegisteredTypes[viewModelType] = pageType;
        public static void RegisterPage<TViewModel, TPage>() where TPage : Page where TViewModel : ViewModel
            => RegisteredTypes[typeof(TViewModel)] = typeof(TPage);

        public static Page GetPage(ViewModel model, bool singleton = true)
        {
            var page = GetPage(model.GetType(), singleton);
            page.BindingContext = model;
            return page;
        }

        public static T GetPage<T>(ViewModel model, bool singleton = true) where T : Page
            => (T)GetPage(model, singleton);

        public static Page GetPage<T>(bool singleton = true)
            => GetObject<T, Page>(singleton);

        public static Page GetPage(Type type, bool singleton = true)
            => GetObject<Page>(type, singleton);

        public static T GetObject<T>(bool singleton = true)
            => GetObject<T, T>(singleton);

        public static T1 GetObject<T, T1>(bool singleton = true)
            => GetObject<T1>(typeof(T), singleton);

        public static T GetObject<T>(Type type, bool singleton = true)
        {
            type = type ?? throw new NullReferenceException("NavigationMap.GetObject('type' param is null)");

            Console.WriteLine($@"[NavigationMap:GetObject<T>(Type type, bool singleton = true)] T:{typeof(T)} type:{type.FullName} singleton:{singleton}");

            if (!RegisteredTypes.TryGetValue(type, out Type objectType))
                return default(T);

            if (!singleton)
                return (T)Activator.CreateInstance(objectType);

            if (!Singletons.TryGetValue(objectType, out object item))
            {
                Console.WriteLine($@"[NavigationMap:GetObject<T>(Type type, bool singleton = true)] Activating Instance of [{objectType}]");

                item = (T)Activator.CreateInstance(objectType);
                Singletons[objectType] = item;
            }

            Console.WriteLine($@"[NavigationMap:GetObject<T>(Type type, bool singleton = true)] Navigating");

            return (T)item;
        }
    }
}