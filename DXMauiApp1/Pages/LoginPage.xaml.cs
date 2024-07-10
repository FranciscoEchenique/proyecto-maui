using DXMauiApp1.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DXMauiApp1.Pages;

public partial class LoginPage : ContentPage
{
    LoginPageModel viewModel;
    public LoginPage()
	{
		InitializeComponent();
        viewModel = new LoginPageModel();
        BindingContext = viewModel;
	}

}
