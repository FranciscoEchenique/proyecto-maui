using DXMauiApp1.Dtos;

namespace DXMauiApp1
{
    public partial class App : Application
    {
        public static bool IsLoged { get; set; } = false;
        public static string Token { get; set; }
        public static Usuario Usuario { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            string username = Preferences.Get("Username", null);

            if (!string.IsNullOrEmpty(username))
            {
                Usuario = new Usuario() { UserName = username};
                Token = Preferences.Get("Token", null);
                IsLoged = true;
                Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}