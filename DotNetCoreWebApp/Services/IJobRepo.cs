using DotNetCoreWebApp.Models;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Services
{
    public interface IJobRepo
    {
        Task<int> Job(JobDetailDto jobDetailDto);
    }
}
