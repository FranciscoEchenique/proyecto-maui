using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DXMauiApp1.Dtos;
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
        private string _url = "https://70q8bmkn-44342.brs.devtunnels.ms/api/Authentication/Authenticate";
        private string _token;
        private HttpClient _httpClient = new HttpClient();


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

                string json = $@"{{""userName"": ""{Username}"", ""password"": ""{Password}"" }}";
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(token))
                    {
                        await Shell.Current.DisplayAlert("Login error", "Invalid credentials.", "OK");
                        Loading = false;
                        Opacity = 1;
                        return;
                    }

                    _token = token;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var usuario = new Usuario()
                    {
                        UserName = Username
                    };

                    App.Usuario = usuario;
                    App.Token = _token;

                    Preferences.Set("Username", Username);
                    Preferences.Set("Token", _token);

                    Loading = false;
                    Opacity = 1;
                    Username = string.Empty;
                    Password = string.Empty;
                    IsPasswordTouched = false;
                    IsUsernameTouched = false;

                    await Shell.Current.DisplayAlert("Login", "Login successful!", "OK");
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    Opacity = 1;
                    Loading = false;
                    await Shell.Current.DisplayAlert("Login error", "Invalid credentials.", "OK");
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
