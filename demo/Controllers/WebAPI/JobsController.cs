using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using demo.Classes;
using demo.Models;

namespace tfcupdated.Controllers.WebAPI
{
    public class JobsController : ApiController
    {
        
        

        //Get element by id
        public class GetDTO
        {
            public int id;
            public string jobcode;
            public string jobtitle;
            public string qualification;
            public string description;
            public DateTime _lastdtappy;
            public string lastdtapply;
            
        }

        [HttpGet]
        public IHttpActionResult GetJobById(int id)
        {
            //string ticket = "";
            //if (Request.Headers.Authorization != null)
            //    ticket = Request.Headers.Authorization.ToString();
            //if (ticket == null || ticket == "" || Authentication.AuthenticateToken(ticket, out User user) == false)
            //    return BadRequest("Your are not logged in !");
            
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var job = (from j in db.JobApplications.Where(a => a.Id == id)
                                select new GetDTO
                                {
                                    id = j.Id,
                                    jobcode = j.JobCode,
                                    jobtitle = j.JobTitle,
                                    qualification = j.MinQualification,
                                    description = j.Description,
                                    _lastdtappy = j.ApplyLastDate,
                                    lastdtapply="",
                                }).FirstOrDefault();

                    job.lastdtapply = job._lastdtappy.ToString("MM/dd/yyyy");
                    
                    return Ok(job);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
                
            }
        }
    }
}
