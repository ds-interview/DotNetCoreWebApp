using DotNetCoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Repository
{
    public interface ILogin
    {
        Task<IEnumerable<UserLogin>> getuser();
        Task<UserLogin> AuthenticateUser(string username, string passcode);
    }
}
