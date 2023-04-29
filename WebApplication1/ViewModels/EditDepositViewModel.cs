using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class EditDepositViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is requied")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is requied")]
        public string? LastName { get; set; }
        [Phone]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? Phone { get; set; }
        public string? Place { get; set; }
        public string? Icon { get; set; }
        [Required(ErrorMessage = "Food is requied")]
        public string? Food { get; set; }
        [Required(ErrorMessage = "PlaceDeliver is requied")]
        public string? PlaceDeliver { get; set; }
        [Required(ErrorMessage = "MaxTimePose is requied")]
        public int? MaxTimePose { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
        public string Email { get; set; }
        public string? StatePost { get; set; }
        public string? AppUserId { get; set; }
    }
}
