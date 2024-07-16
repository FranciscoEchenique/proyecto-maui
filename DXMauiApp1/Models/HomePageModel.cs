using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Core;
using DXMauiApp1.Dtos;
using DXMauiApp1.Services;
using DXMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Models
{
    public partial class HomePageModel : ObservableObject
    {
        private readonly IApiService<Anunciante> _apiService;

        [ObservableProperty]
        private IEnumerable<Anunciante> _anunciantes;

        [ObservableProperty]
        private bool _isRefreshing = false;

        public HomePageModel(IApiService<Anunciante> apiService)
        {
            _apiService = apiService;
            new Action(async () => await GetDataAsync())();
        }

        private async Task GetDataAsync()
        {
            try
            {
                IsRefreshing = true;
                var anunciantes = await _apiService.GetAll();

                if (anunciantes != null)
                    Anunciantes = anunciantes;
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        [RelayCommand]
        public async Task PullToRefresh()
        {
            await GetDataAsync();
            IsRefreshing = false;
        }

        public async Task UpdateAnunciante(object item)
        {
            try
            {
                Anunciante anunciante = (Anunciante)item;
                await _apiService.Update(anunciante, anunciante.IdAnunciante);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public async Task DeleteAnunciante(object item)
        {
            try
            {
                Anunciante anunciante = (Anunciante)item;
                await _apiService.Delete(anunciante.IdAnunciante);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
