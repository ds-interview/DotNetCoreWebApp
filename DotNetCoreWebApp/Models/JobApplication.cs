using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        public int JobCode { get; set; }
        public string Title { get; set; }
        public string MinimumQualification { get; set; }
        public string SortDescription { get; set; }
        public DateTime LastDate { get; set; }
    }
}
