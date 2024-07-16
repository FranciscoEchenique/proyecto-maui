using DXMauiApp1.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DXMauiApp1.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginPageModel _viewModel;
    public LoginPage(LoginPageModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        Shell.SetNavBarIsVisible(this, false);
    }

}
