using DotNetCoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetCoreWebApp.Repository
{
    public class JobApplicationDetail : IJobApplication
    {
        private readonly JobAppDbcontext _context;

        public JobApplicationDetail(JobAppDbcontext context)
        {
            _context = context;
        }

        public async Task<int> CreateJobApplication(JobApplication jobApplication)
        {
            if (jobApplication != null)
            {
                await _context.JobApplication.AddAsync(jobApplication);
                await _context.SaveChangesAsync();
                return jobApplication.Id;
            }
            return 0;
        }

        public async Task<int> DeleteJobApplication(int? id)
        {
            int Getresult = 0;

            if (id != null)
            {
                var rec_id = await _context.JobApplication.FirstOrDefaultAsync(JobAppId => JobAppId.Id == id);
                if (rec_id != null)
                {
                    _context.Remove(_context.JobApplication.Single(JobAppId => JobAppId.Id == id));
                    Getresult = await _context.SaveChangesAsync();
                }
                return Getresult;

            }
            return Getresult;
        }


        public async Task<int> UpdateJobApplication(JobApplication jobApplication)
        {
            if (jobApplication != null)
            {
                _context.JobApplication.Update(jobApplication);
                await _context.SaveChangesAsync();
                return jobApplication.Id;
            }
            return 0;
        }

        
        public async Task<IEnumerable<JobApplication>> GetJobApplication()
        {
            if (_context != null)
            {
                return await _context.JobApplication.ToListAsync();
            }

            return null;
        }

        public JobApplication GetJobApplicationById(int id)
        {

            if (_context != null)
            {
                return _context.JobApplication.FirstOrDefault(e => e.Id == id);

            }
            return null;
        }
    }
}