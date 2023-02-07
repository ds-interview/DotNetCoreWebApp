using DotNetCoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Repository
{
    public interface IJobApplication
    {
        Task<int> CreateJobApplication(JobApplication jobApplication);
        Task<int> UpdateJobApplication(JobApplication jobApplication);
        Task<int> DeleteJobApplication(int? id);
        JobApplication GetJobApplicationById(int id);
        Task<IEnumerable<JobApplication>> GetJobApplication();

    }
}
