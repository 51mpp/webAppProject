using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Comfirm password")]
        [Required(ErrorMessage = "Comfirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "FirstName is requied")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is requied")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }
        public string? NickName { get; set; }
        public int? Section { get; set; }
        public string? Icon { get; set; }
        public string? Account { get; set; }
        public IFormFile Image { get; set; }
    }
}