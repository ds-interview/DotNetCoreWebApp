using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApp.Models
{
    public class JobAppDbcontext : DbContext
    {
        public JobAppDbcontext(DbContextOptions<JobAppDbcontext> options) : base(options)
        {

        }
        public DbSet<JobApplication> JobApplication { get; set; }


    }
}