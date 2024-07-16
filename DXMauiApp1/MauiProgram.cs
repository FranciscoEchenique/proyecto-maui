using DevExpress.Maui;
using DevExpress.Maui.Core;
using DXMauiApp1.Models;
using DXMauiApp1.Pages;
using DXMauiApp1.Services;
using DXMauiApp1.Services.Interfaces;
using DXMauiApp1.Views;

namespace DXMauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThemeManager.ApplyThemeToSystemBars = true;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpressGauges()
                .UseDevExpress(useLocalization: true)
                .UseDevExpressControls()
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressEditors()
                .UseDevExpressDataGrid()
                .UseDevExpressScheduler()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                    fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                });

            builder
                .RegisterViews()
                .RegisterViewModels()
                .RegisterServices();

            DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();
            DevExpress.Maui.DataGrid.Initializer.Init();
            DevExpress.Maui.Scheduler.Initializer.Init();
            return builder.Build();
        }
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IHttpClientService, HttpClientService>();
            mauiAppBuilder.Services.AddSingleton(typeof(IApiService<>), typeof(ApiService<>));
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
            mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<LoginPageModel>();
            mauiAppBuilder.Services.AddSingleton<MainPageModel>();
            mauiAppBuilder.Services.AddSingleton<HomePageModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();
            mauiAppBuilder.Services.AddSingleton<LoginPage>();
            //mauiAppBuilder.Services.AddSingleton<HomeContent>();

            return mauiAppBuilder;
        }
    }

}