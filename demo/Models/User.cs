using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "User name is required!")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Salt { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
