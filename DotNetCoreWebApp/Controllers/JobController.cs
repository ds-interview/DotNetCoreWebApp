using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            JobDetailDto jobDetailDto = new JobDetailDto();
            return View(jobDetailDto);
        }

        public IActionResult Jobs()
        {
            return View();
        }
    }
}
