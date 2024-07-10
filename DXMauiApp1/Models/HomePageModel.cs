using CommunityToolkit.Mvvm.ComponentModel;
using DXMauiApp1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Models
{
    public partial class HomePageModel : ObservableObject
    {
        [ObservableProperty]
        private Hora _hora;


    }
}
