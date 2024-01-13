using DotNetCoreWebApp.Models;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Services
{
    public interface ILoginRepo
    {
        Task<UserDto> ValidateLoginAsync(UserDto userDto);
        Task<int> SignUpUser(SignUpDto signUpDto);
    }
}
