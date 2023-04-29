using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class EditMainPoseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is requied")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is requied")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Phone is requied")]
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
        public string? URLImage { get; set; }
        [Required(ErrorMessage = "Place is required.")]
        public string? Place { get; set; }
        [Required(ErrorMessage = "MaxComment is requied")]
        public int? MaxComment { get; set; }
        [Required(ErrorMessage = "MaxTimePose is requied")]
        public int? MaxTimePose { get; set; }
        public string? Account { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Email { get; set; }
        public string? StatePost { get; set; }
        public string? AppUserId { get; set; }
        public string? Icon { get; set; }
    }
}
