using Dapper;
using DotNetCoreWebApp.DataServices;
using DotNetCoreWebApp.Models;
using System.Collections.Generic;
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
                        jobDetailDto.JobDetailId,
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

        public async Task<int> ModifyJob(JobDetailDto jobDetailDto)
        {
            try
            {
                using var connection = dapperConnectionProvider.Connect();
                var result = await connection.ExecuteAsync("USP_JobDetail",
                    new
                    {
                        jobDetailDto.JobDetailId,
                        jobDetailDto.JobCode,
                        jobDetailDto.Title,
                        jobDetailDto.MinimumQualification,
                        jobDetailDto.SortDescription,
                        jobDetailDto.LastDate,
                        jobDetailDto.CreatedBy,
                        jobDetailDto.ModifiedBy,
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

        public async Task<List<JobDetailDto>> GetAllJobs()
        {
            using var connection = dapperConnectionProvider.Connect();
            var jobDetailsList = await connection.QueryAsync<JobDetailDto>("USP_Get_All_Jobs",
                commandType: CommandType.StoredProcedure);

            return (List<JobDetailDto>)jobDetailsList;
        }

        public async Task<JobDetailDto> GetJobByJobId(int jobId)
        {
            using var connection = dapperConnectionProvider.Connect();
            var jobDetails = await connection.QueryFirstOrDefaultAsync<JobDetailDto>("USP_Get_JobDetail_By_Id",
                new
                {
                    jobId
                },
                commandType: CommandType.StoredProcedure);

            return jobDetails;
        }
    }
}
