using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApp.Models
{
    public class LoginDbcontext : DbContext
    {
        public LoginDbcontext(DbContextOptions<LoginDbcontext> options)
        : base(options)
        {

        }
        public DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLogin>(entity => {
                entity.HasKey(k => k.id);
            });
        }
    }
}