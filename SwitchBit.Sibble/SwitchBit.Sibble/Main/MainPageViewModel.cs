using SwitchBit.Sibble.Movers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwitchBit.Sibble
{
    public class Person
    {
        public string Name { get; set; }
        public double PercentPortfolio { get; set; }

        public double Amount { get; set; }
        public string Exchange { get; set; }
    }

    public class MainPageViewModel
    {
        public ICommand ChainsCommand { get; set; } = new Command(async () => await ExecuteChainsCommand());
        
        public ICommand MoversCommand { get; set; } = new Command(async () => await ExecuteMoversCommand());

        private static async Task ExecuteMoversCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new MoversPage());
        }

        private static async Task ExecuteChainsCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ChainsPage());
        }

        public List<Person> Data { get; set; }

        public MainPageViewModel()
        {
            Data = new List<Person>()
            {
                new Person { Name = "$BTC", PercentPortfolio = 51, Exchange="Bittrex", Amount=1.32542738D },
                new Person { Name = "$LTC", PercentPortfolio = 9, Exchange="Poloniex", Amount=12.62242738D },
                new Person { Name = "$BCC", PercentPortfolio = 24, Exchange="HitBTC", Amount=214.99872738D},
                new Person { Name = "$ETH", PercentPortfolio = 16, Exchange="Bitmex", Amount=1.32542738D },
                new Person { Name = "$BTC", PercentPortfolio = 51, Exchange="Bittrex", Amount=1.32542738D },
                new Person { Name = "$LTC", PercentPortfolio = 9, Exchange="Poloniex", Amount=12.62242738D },
                new Person { Name = "$BCC", PercentPortfolio = 24, Exchange="HitBTC", Amount=214.99872738D},
                new Person { Name = "$ETH", PercentPortfolio = 16, Exchange="Bitmex", Amount=1.32542738D }
            };
        }
    }
}
