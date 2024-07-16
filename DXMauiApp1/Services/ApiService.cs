using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static DXMauiApp1.Dtos.ResultDto;
using DXMauiApp1.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using DXMauiApp1.Pages;

namespace DXMauiApp1.Services
{
    public class ApiService<T> : IApiService<T> where T : class
    {
        private HttpClient _httpClient = new();

        private string _token = App.Token;

        private string _baseUrl = ApiUrls.ApiUrl + ApiUrls.ODataPrefix + "/" + typeof(T).Name;
        public ApiService()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            GetToken();
            var response = await _httpClient.GetStringAsync(_baseUrl);

            var result = JsonConvert.DeserializeObject<ResultDTO<IEnumerable<T>>>(response);

            return result.value;  
        }

        public async Task Update(T item, object key)
        {

            string url = _baseUrl + $"({key})";

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            await _httpClient.PatchAsync(url, httpContent);
        }

        public async Task Delete(object key)
        {
            string url = _baseUrl + $"({key})";

            await _httpClient.DeleteAsync(url);
        }

        private void GetToken()
        {
            if(_token != App.Token)
            {
                _token = App.Token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }

        }
    }
}
