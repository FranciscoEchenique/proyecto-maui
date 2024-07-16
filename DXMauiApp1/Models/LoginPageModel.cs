using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DXMauiApp1.Dtos;
using DXMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Models
{
    public partial class LoginPageModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        private readonly IAuthService _authService;
        public LoginPageModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UsernameErrorText))]
        [NotifyPropertyChangedFor(nameof(UsernameHasError))]
        private string _username = "";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordErrorText))]
        [NotifyPropertyChangedFor(nameof(PasswordHasError))]
        private string _password = "";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UsernameErrorText))]
        [NotifyPropertyChangedFor(nameof(UsernameHasError))]
        private bool _isUsernameTouched;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordErrorText))]
        [NotifyPropertyChangedFor(nameof(PasswordHasError))]
        private bool _isPasswordTouched;

        [ObservableProperty]
        private bool _loading = false;

        [ObservableProperty]
        private decimal _opacity = 1;

        public bool UsernameHasError => !string.IsNullOrWhiteSpace(UsernameErrorText);

        public bool PasswordHasError => !string.IsNullOrWhiteSpace(PasswordErrorText);

        public string UsernameErrorText => string.IsNullOrWhiteSpace(Username) && IsUsernameTouched ? "Username is required" : string.Empty;

        public string PasswordErrorText => string.IsNullOrWhiteSpace(Password) && IsPasswordTouched ? "Password is required" : string.Empty;

        [RelayCommand]
        public void UsernameTouched() => IsUsernameTouched = true;

        [RelayCommand]
        public void PasswordTouched() => IsPasswordTouched = true;

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                Loading = true;
                Opacity = 0.5m;

                (bool, string) loginResponse = await _authService.Login(Username, Password);

                bool responseSuccessful = loginResponse.Item1;

                string token = loginResponse.Item2;

                if (responseSuccessful)
                {
                    
                    if (string.IsNullOrEmpty(token))
                    {
                        await Shell.Current.DisplayAlert("Login error", "Invalid credentials.", "OK");
                        Loading = false;
                        Opacity = 1;
                        return;
                    }

                    var usuario = new Usuario()
                    {
                        UserName = Username
                    };

                    App.Usuario = usuario;
                    App.Token = token;
                    App.IsLoged = true;

                    Preferences.Set("Username", Username);
                    Preferences.Set("Token", token);

                    Loading = false;
                    Opacity = 1;
                    Username = string.Empty;
                    Password = string.Empty;
                    IsPasswordTouched = false;
                    IsUsernameTouched = false;

                    await Shell.Current.DisplayAlert("Login", "Login successful!", "OK");
                    await _navigationService.NavigateTo<MainPage>(true);
                }
                else
                {
                    Opacity = 1;
                    Loading = false;
                    await Shell.Current.DisplayAlert("Login error", "Internal server error.", "OK");
                }
            }
            catch(Exception ex)
            {
                Opacity = 1;
                Loading = false;
                await Shell.Current.DisplayAlert("Login error", ex.Message, "OK");
            }
           
        }
    }
}
