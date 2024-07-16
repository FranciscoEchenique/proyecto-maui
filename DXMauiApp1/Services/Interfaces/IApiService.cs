using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services.Interfaces
{
    public interface IApiService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Update(T item, object key);
        Task Delete(object key);
    }
}
