using DotNetCoreWebApp.Models;
using DotNetCoreWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApiController : ControllerBase
    {
        private readonly IJobRepo _jobRepo;
        public JobApiController(IJobRepo jobRepo)
        {
            _jobRepo = jobRepo;
        }

        [HttpPost]
        [Route("Job")]
        public async Task<IActionResult> Job(JobDetailDto jobDto)
        {
            if (jobDto.JobCode == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Job Code is required");

            if (jobDto.Title == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Title is required");

            if (!string.IsNullOrEmpty(jobDto.strLastDate))
                jobDto.LastDate = Convert.ToDateTime(jobDto.strLastDate);

            var result = await _jobRepo.Job(jobDto);
            if (result > 0)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
                return StatusCode(StatusCodes.Status204NoContent, "No Data");
        }
    }
}
