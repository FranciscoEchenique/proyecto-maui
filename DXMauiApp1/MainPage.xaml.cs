using DXMauiApp1.Models;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DXMauiApp1
{
    public partial class MainPage : ContentPage
    {
        MainPageModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Username = App.Usuario.UserName;
            UsernameLabel.Text = viewModel.Username;

        }
        async void OnOpenWebButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync("https://www.devexpress.com/maui/");
        }

       
    }


}