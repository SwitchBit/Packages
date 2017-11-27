using System;
using System.Collections.Generic;
using System.Text;

//NOTE: Stole this code fair and square from James Montemagno, no sense reinventing
// https://github.com/jamesmontemagno/mvvm-helpers/blob/master/MvvmHelpers

namespace SwitchBit.Xaml
{
    public class Grouping<TKey, TItem> : ObservableRangeCollection<TItem>
    {
        public TKey Key { get; }
        public new IList<TItem> Items => base.Items;
        public Grouping(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            AddRange(items);
        }
    }

    public class Grouping<TKey, TSubKey, TItem> : ObservableRangeCollection<TItem>
    {
        public TKey Key { get; }
        public TSubKey SubKey { get; }
        public new IList<TItem> Items => base.Items;
        public Grouping(TKey key, TSubKey subkey, IEnumerable<TItem> items)
        {
            Key = key;
            SubKey = subkey;
            AddRange(items);
        }
    }
}