using demo.Classes;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            if (Session["username"] == null || Session["userid"] == null || Session["ticket"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (Authentication.AuthenticateToken(Session["ticket"].ToString(), out User user) == false)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        
        public ActionResult Job()
        {
            if (Session["username"] == null || Session["userid"] == null || Session["ticket"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (Authentication.AuthenticateToken(Session["ticket"].ToString(), out User user) == false)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult JobApplication(JobApplication job)
        {
            if (Session["username"] == null || Session["userid"] == null || Session["ticket"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (Authentication.AuthenticateToken(Session["ticket"].ToString(), out User user) == false)
            {
                return RedirectToAction("Login", "Login");
            }

            if (string.IsNullOrWhiteSpace(job.JobCode))
            {
                TempData["error"] = "Please enter Job Code!";
                return RedirectToAction("Job");
            }
            if (string.IsNullOrWhiteSpace(job.JobTitle))
            {
                TempData["error"] = "Please enter Job Title!";
                return RedirectToAction("Job");
            }
            if (string.IsNullOrWhiteSpace(job.MinQualification))
            {
                TempData["error"] = "Please enter minimum qualification!";
                return RedirectToAction("Job");
            }
            if (string.IsNullOrWhiteSpace(job.Description))
            {
                TempData["error"] = "Please enter short description!";
                return RedirectToAction("Job");
            }
            if (job.ApplyLastDate == null)
            {
                TempData["error"] = "Please enter short description!";
                return RedirectToAction("Job");
            }
            try
            {
                if (job.Id == 0)
                {
                    if(db.JobApplications.Where(a => a.JobCode.ToLower() == job.JobCode.ToLower() && a.UserId == user.Id).Count() > 0)
                    {
                        TempData["error"] = "This job code already exist, Please try another job code!";
                        return RedirectToAction("Job");
                    }
                    job.UserId = user.Id;
                    job.CreatedOn = DateTime.Now;
                    db.JobApplications.Add(job);
                    db.SaveChanges();
                    TempData["success"] = "Job successfully added !";
                    return RedirectToAction("Job");

                }
                else
                {
                    var jobobj = db.JobApplications.Where(a => a.Id == job.Id).FirstOrDefault();
                    if(jobobj != null)
                    {
                        jobobj.JobCode = job.JobCode;
                        jobobj.JobTitle = job.JobTitle;
                        jobobj.MinQualification = job.MinQualification;
                        jobobj.Description = job.Description;
                        jobobj.ApplyLastDate = job.ApplyLastDate;
                        jobobj.UpdatedOn = DateTime.Now;
                        db.Entry(jobobj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["success"] = "Job successfully updated !";
                        return RedirectToAction("Job");
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Job");
            }

            return View();
        }


        public class GetDto
        {
            public int Id;
            public string JobCode;
            public string JobTitle;
            public string MinQualification;
            public string Description;
            public DateTime _ApplyLastDate;
            public string ApplyLastDate;
        }

        public ActionResult Jobs()
        {
            if (Session["username"] == null || Session["userid"] == null || Session["ticket"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (Authentication.AuthenticateToken(Session["ticket"].ToString(), out User user) == false)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                //IEnumerable<JobApplication> Joblist = db.JobApplications;

                var Joblist = (from j in db.JobApplications.Where(a => a.UserId == user.Id && a.DeletedOn == null)
                               select new GetDto
                               {
                                   Id = j.Id,
                                   JobCode = j.JobCode,
                                   JobTitle = j.JobTitle,
                                   MinQualification = j.MinQualification,
                                   Description = j.Description,
                                   _ApplyLastDate = j.ApplyLastDate,
                                   ApplyLastDate = ""
                               }).OrderBy(a => a.Id).ToList();
                foreach(var job in Joblist)
                {
                    job.ApplyLastDate = job._ApplyLastDate.ToString("MMM, dd, yyyy");
                }

                ViewBag.Jobs = Joblist;
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            
            return View();
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["username"] == null || Session["userid"] == null || Session["ticket"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (Authentication.AuthenticateToken(Session["ticket"].ToString(), out User user) == false)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var job = db.JobApplications.Where(a => a.Id == id && a.UserId == user.Id).FirstOrDefault();
                    
                    if (job != null)
                    {
                        //db.JobApplications.Remove(job);
                        //db.SaveChanges();
                        job.DeletedOn = DateTime.Now;
                        db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["success"] = "job successfully deleted !";
                        return RedirectToAction("Jobs");
                    }
                    else
                    {
                        TempData["error"] = "Job not found. job have already removed or deleted.";
                        return RedirectToAction("Jobs");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Jobs");
            }

        }
    }
}