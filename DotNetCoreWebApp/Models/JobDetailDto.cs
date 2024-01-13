using System;

namespace DotNetCoreWebApp.Models
{
    public class JobDetailDto
    {
        public int JobDetailId { get; set; }
        public string JobCode { get; set; }
        public string Title { get; set; }
        public string MinimumQualification { get; set; }
        public string SortDescription { get; set; }
        public string strLastDate { get; set; }
        public DateTime LastDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByName { get; set; }
        public int SpType { get; set; }
    }
}