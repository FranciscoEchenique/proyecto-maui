using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateTo<TPageType>() where TPageType : ContentPage;
        Task NavigateTo<TPageType>(bool isAbsoluteRoute) where TPageType : ContentPage;
    }
}
