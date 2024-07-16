using DevExpress.Maui.Core;
using DevExpress.Maui.Editors;
using DXMauiApp1.Models;

namespace DXMauiApp1.Views;

public partial class HomeContent : ContentView
{
	private HomePageModel _viewModel;
	public HomeContent()
	{
		InitializeComponent();
        HandlerChanged += OnHandlerChanged;
	}

    private void OnHandlerChanged(object sender, EventArgs e)
    {
        _viewModel = Handler.MauiContext.Services.GetRequiredService<HomePageModel>();
        BindingContext = _viewModel;
    }

    private void grid_DetailFormShowing(object sender, DetailFormShowingEventArgs e)
    {
        if(e.ViewModel is DetailFormViewModel viewModel) 
        {
            DetailFormPage page = (DetailFormPage)e.Content;
            page.DeleteToolbarButton.Clicked += DeleteToolbarButton_Clicked;
        }

        if (e.ViewModel is not DetailEditFormViewModel editViewModel)
            return;
        DetailEditFormPage form = (DetailEditFormPage)e.Content;
        form.SaveButton.Content = "Guardar";
        form.SaveButton.Clicked += SaveButton_Clicked;
        form.TitleLabel.Text = "Editar";
        form.DeleteToolbarButton.Clicked += DeleteToolbarButton_Clicked;
        
    }

    private async void DeleteToolbarButton_Clicked(object sender, EventArgs e)
    {
        object item = GridView.SelectedItem;
        await _viewModel.DeleteAnunciante(item);
       
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void ValidateAndSave(object sender, ValidateItemEventArgs e)
	{
		await _viewModel.UpdateAnunciante(e.Item);
	}
}