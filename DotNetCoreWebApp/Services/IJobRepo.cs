using DotNetCoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Services
{
    public interface IJobRepo
    {
        Task<int> Job(JobDetailDto jobDetailDto);
        Task<List<JobDetailDto>> GetAllJobs();
        Task<JobDetailDto> GetJobByJobId(int jobId);
        Task<int> UpdateJob(JobDetailDto jobDetailDto);
        Task<int> DeleteJob(JobDetailDto jobDetailDto);
    }
}
