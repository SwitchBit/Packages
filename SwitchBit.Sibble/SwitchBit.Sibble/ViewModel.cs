using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Sibble
{
    public class Person
    {
        public string Name { get; set; }
        public double PercentPortfolio { get; set; }

        public double Amount { get; set; }
        public string Exchange { get; set; }
    }

    public class ViewModel
    {
        public List<Person> Data { get; set; }

        public ViewModel()
        {
            Data = new List<Person>()
            {
                new Person { Name = "$BTC", PercentPortfolio = 51, Exchange="Bittrex", Amount=1.32542738D },
                new Person { Name = "$LTC", PercentPortfolio = 9, Exchange="Poloniex", Amount=12.62242738D },
                new Person { Name = "$BCC", PercentPortfolio = 24, Exchange="HitBTC", Amount=214.99872738D},
                new Person { Name = "$ETH", PercentPortfolio = 16, Exchange="Bitmex", Amount=1.32542738D }
            };
        }
    }
}
