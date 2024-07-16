using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DXMauiApp1.Pages;
using DXMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Models
{
    public partial class MainPageModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        private readonly IAuthService _authService;
        public MainPageModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        [ObservableProperty]
        string _username;

        [RelayCommand]
        public async Task Logout()
        {
            _authService.Logout();
            await _navigationService.NavigateTo<LoginPage>(true);
        }
    }
}
