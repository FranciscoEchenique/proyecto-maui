using DXMauiApp1.Models;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DXMauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageModel _viewModel;
        public MainPage(MainPageModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Username = App.Usuario.UserName;
            UsernameLabel.Text = _viewModel.Username;
        }

       
    }


}