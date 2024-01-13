using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
