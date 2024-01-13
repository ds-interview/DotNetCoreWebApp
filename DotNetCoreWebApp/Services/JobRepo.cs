using Dapper;
using DotNetCoreWebApp.DataServices;
using DotNetCoreWebApp.Models;
using System.Data;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Services
{
    public class JobRepo: IJobRepo
    {
        private readonly DapperConnectionProvider dapperConnectionProvider;

        public JobRepo(DapperConnectionProvider dapperConnectionProvider)
        {
            this.dapperConnectionProvider = dapperConnectionProvider;
        }

        public async Task<int> Job(JobDetailDto jobDetailDto)
        {
            try
            {
                using var connection = dapperConnectionProvider.Connect();
                var result = await connection.ExecuteScalarAsync<int>("USP_JobDetail",
                    new
                    {
                        jobDetailDto.JobCode,
                        jobDetailDto.Title,
                        jobDetailDto.MinimumQualification,
                        jobDetailDto.SortDescription,
                        jobDetailDto.LastDate,
                        jobDetailDto.CreatedBy,
                        jobDetailDto.SpType
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
