using DotNetCoreWebApp.Models;
using DotNetCoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepo _jobRepo;
        public JobController(IJobRepo jobRepo)
        {
            _jobRepo = jobRepo;
        }

        public async Task<IActionResult> Index(int id = 0)
        {
            JobDetailDto jobDetailDto = new JobDetailDto();
            if (id > 0)
                jobDetailDto = await _jobRepo.GetJobByJobId(id);

            return View(jobDetailDto);
        }

        public IActionResult Jobs()
        {
            return View();
        }
    }
}
