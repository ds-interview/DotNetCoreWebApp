using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using DotNetCoreWebApp.Repository;
using DotNetCoreWebApp.ViewModel;

namespace DotNetCoreWebApp.Controllers
{
    public class HomeControllerJobApplication : Controller
    {
        private readonly IJobApplication jobApplication;

        public HomeControllerJobApplication(IJobApplication jobApplication)
        {
            this.jobApplication = jobApplication;
        }

        public IActionResult Index()
        {
            try
            {
                var GetJobAppresult = jobApplication.GetJobApplication().Result;
                ViewBag.JobApplication = GetJobAppresult;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobApplication jobApplicationmodel)
        {
            if (ModelState.IsValid)
            {
                jobApplication.CreateJobApplication(jobApplicationmodel);
            }
            ViewBag.message = "Data Saved Successfully..";

            ModelState.Clear();
            return View();
        }

        public IActionResult Edit(int id)
        {
            JobApplicationViewmodel jobAppview = new JobApplicationViewmodel()
            {
                viewJobApplication = jobApplication.GetJobApplicationById(id)
            };
            return View(jobAppview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobApplicationViewmodel viewJobApplication)
        {
            JobApplication JobAppmodel = new JobApplication()
            {
                Id = viewJobApplication.Id,
                JobCode = viewJobApplication.JobCode,
                Title = viewJobApplication.Title,
                MinimumQualification = viewJobApplication.MinimumQualification,
                SortDescription = viewJobApplication.SortDescription,
                LastDate =(DateTime)viewJobApplication.LastDate
            };
            var result = jobApplication.UpdateJobApplication(JobAppmodel);
            ModelState.Clear();
            return View();
        }

        public IActionResult Delete(int id)
        {
            JobApplicationViewmodel jobAppview = new JobApplicationViewmodel()
            {
                viewJobApplication = jobApplication.GetJobApplicationById(id)
            };
            return View(jobAppview);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(JobApplicationViewmodel viewmodel)
        {
            var result = jobApplication.DeleteJobApplication(viewmodel.Id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}