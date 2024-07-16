using DXMauiApp1.Pages;
using DXMauiApp1.Services.Interfaces;
using Java.Net;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services
{
    public class AuthService : IAuthService
    {
        private readonly INavigationService _navigationService;

        private HttpClient _httpClient = new();

        private string _token = string.Empty;

        private string _url = $"{ApiUrls.ApiUrl}/Authentication/Authenticate";


        public AuthService(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }

        public async Task<(bool, string)> Login(string username, string password)
        {
            string json = $@"{{""userName"": ""{username}"", ""password"": ""{password}"" }}";
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                _token = token;

                return (true, token);
            }
            else
            {
                return (false, string.Empty);
            }
        }
        public void Logout()
        {
            Preferences.Remove("Token");
            Preferences.Remove("Username");

            App.IsLoged = false;
            App.Usuario = null;
            App.Token = string.Empty;
        }
        public async Task<bool> ValidateIsLoged(string token)
        {
            if (!App.IsLoged)
            {
                await _navigationService.NavigateTo<LoginPage>(true);
                return false;
            }
            else
            {
                if (!VerifyTokenExpires(token))
                {
                    return true;
                }
                else
                {
                    Logout();
                    await _navigationService.NavigateTo<LoginPage>(true);
                    return false;
                }
            }
        }
        private bool VerifyTokenExpires(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            var exp = jwtToken.ValidTo;

            return DateTime.Now > exp;
        }
    }
}
