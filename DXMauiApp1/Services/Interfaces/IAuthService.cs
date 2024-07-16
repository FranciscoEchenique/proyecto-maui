using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool, string)> Login(string username, string password);
        void Logout();
        Task<bool> ValidateIsLoged(string token);
    }
}
