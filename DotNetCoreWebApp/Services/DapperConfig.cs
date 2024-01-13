using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DotNetCoreWebApp.DataServices
{
    public class DapperConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Jobs");
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public IDbConnection Connect()
            => new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
    }
}