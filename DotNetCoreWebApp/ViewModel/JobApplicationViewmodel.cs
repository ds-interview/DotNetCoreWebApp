using DotNetCoreWebApp.Models;
using System;

namespace DotNetCoreWebApp.ViewModel
{
    public class JobApplicationViewmodel
    {
        public JobApplication viewJobApplication { get; set; }
        public int Id { get; set; }
        public int JobCode { get; set; }
        public string? Title { get; set; }
        public string? MinimumQualification { get; set; }
        public string? SortDescription { get; set; }
        public DateTime? LastDate { get; set; }
    }
}
