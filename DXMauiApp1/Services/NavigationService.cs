using DXMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateTo<TPageType>() where TPageType : ContentPage
        {
            await Navigate(typeof(TPageType), false);
        }
        public async Task NavigateTo<TPageType>(bool isAbsoluteRoute) where TPageType : ContentPage
        {
            await Navigate(typeof(TPageType), isAbsoluteRoute);
        }
        private async Task Navigate(Type pageType, bool isAbsoluteRoute = false)
        {
            string pageName = pageType.Name;

            string absolutePrefix = isAbsoluteRoute ? "//" : string.Empty;

            string route = $"{absolutePrefix}{pageName}";

            await Shell.Current.GoToAsync($"{route}");
        }
    }
}
