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
            try
            {
                if (jobDto.JobCode == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Job Code is required");

                if (jobDto.Title == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Title is required");

                if (!string.IsNullOrEmpty(jobDto.strLastDate))
                    jobDto.LastDate = Convert.ToDateTime(jobDto.strLastDate);

                int result;
                if (jobDto.SpType == 1)
                    result = await _jobRepo.Job(jobDto);
                else
                    result = await _jobRepo.ModifyJob(jobDto);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return StatusCode(StatusCodes.Status204NoContent, "No Data");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteJob")]
        public async Task<IActionResult> DeleteJob(JobDetailDto jobDto)
        {
            try
            {
                if (jobDto.JobDetailId == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Job Id not found");

                var result = await _jobRepo.ModifyJob(jobDto);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return StatusCode(StatusCodes.Status204NoContent, "No Data");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                var result = await _jobRepo.GetAllJobs();
                if (result != null || result.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return StatusCode(StatusCodes.Status204NoContent, "No Records Found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Getting Job error " + ex.Message);
            }
        }
    }
}
