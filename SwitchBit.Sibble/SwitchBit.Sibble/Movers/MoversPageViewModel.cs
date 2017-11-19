using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Sibble.Movers
{
    public class MoversPageViewModel : ViewModel
    {
        public List<MoversItem> MoversData { get; set; }

        public MoversPageViewModel()
        {
            MoversData = new List<MoversItem>()
            {
                new MoversItem{TickerSymbol="$BTC"},
                new MoversItem{TickerSymbol="$LTC"},
                new MoversItem{TickerSymbol="$ETH"},
                new MoversItem{TickerSymbol="$ZEC"},
                new MoversItem{TickerSymbol="$NEO"},
                new MoversItem{TickerSymbol="$WAVES"},
                new MoversItem{TickerSymbol="$VTC"},
                new MoversItem{TickerSymbol="$FTC"},
                new MoversItem{TickerSymbol="$QRM"},
                new MoversItem{TickerSymbol="$BCC"},
                new MoversItem{TickerSymbol="$LMN"},
                new MoversItem{TickerSymbol="$ZCS"},
                new MoversItem{TickerSymbol="$TIT"},
            };
        }
    }
}
