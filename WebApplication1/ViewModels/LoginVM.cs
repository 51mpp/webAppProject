using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is requied")]
        public string EmailAddress { get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}