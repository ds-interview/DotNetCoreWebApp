using Dapper;
using DotNetCoreWebApp.DataServices;
using DotNetCoreWebApp.Models;
using DotNetCoreWebApp.Services;
using System.Data;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Service
{
    public class LoginRepo: ILoginRepo
    {
        private readonly DapperConnectionProvider dapperConnectionProvider;

        public LoginRepo(DapperConnectionProvider dapperConnectionProvider)
        {
            this.dapperConnectionProvider = dapperConnectionProvider;
        }

        public async Task<UserDto> ValidateLoginAsync(UserDto userDto)
        {
            using var connection = dapperConnectionProvider.Connect();
            var details = await connection.QueryFirstOrDefaultAsync<UserDto>("USP_Get_Users_By_UserName_Password",
                new
                {
                    userDto.UserName,
                    userDto.Password
                },
                commandType: CommandType.StoredProcedure);

            return details;
        }

        public async Task<int> SignUpUser(SignUpDto signUpDto)
        {
            try
            {
                using var connection = dapperConnectionProvider.Connect();
                var result = await connection.ExecuteScalarAsync<int>("USP_Add_Users",
                    new
                    {
                        signUpDto.Name,
                        signUpDto.Username,
                        signUpDto.Password
                    },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
