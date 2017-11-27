using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Xaml
{
    public interface IDataSource<T>
    {
        Task<bool> AddItem(T item);
        Task<bool> UpdateItem(T item);
        Task<bool> DeleteItem(T item);
        Task<T> GetItem(string id);
        Task<IEnumerable<T>> GetItems(bool forceRefresh = false);

        Task Initialize();
        Task<bool> PullLatest();
        Task<bool> Sync();
    }
}
