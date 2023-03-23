using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace demo.Models
{
    public class UserTicket
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ticket { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
