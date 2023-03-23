using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string JobCode { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(200)]
        public string MinQualification { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime ApplyLastDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
