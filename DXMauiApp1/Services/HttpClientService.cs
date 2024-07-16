using DXMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services
{
    public class HttpClientService : IHttpClientService
    {
        private HttpClient _httpClient = new();

        private string _token = Preferences.Get("Token", "");
        public HttpClientService() 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
        public HttpClient GetHttpClient()
        {
            VerifyTokenExpires();
            return _httpClient;
        }

        private void VerifyTokenExpires()
        {
            var jwtToken = new JwtSecurityToken(_token);
            var exp = jwtToken.ValidTo;

            if(DateTime.Now > exp)
            {

            }
        }
    }
}
