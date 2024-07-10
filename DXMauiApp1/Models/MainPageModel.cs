using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Models
{
    public partial class MainPageModel : ObservableObject
    {
        [ObservableProperty]
        string _username;

        [RelayCommand]
        public async Task Logout()
        {
            Preferences.Remove("Token");
            Preferences.Remove("Username");

            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
