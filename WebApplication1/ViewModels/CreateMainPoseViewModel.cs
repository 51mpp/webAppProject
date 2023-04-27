using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateMainPoseViewModel
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
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Place is required.")]
        public string? Place { get; set; }
        [Required(ErrorMessage = "MaxComment is requied")]
        public int? MaxComment { get; set; }
        public string? Account { get; set; }
        [Required(ErrorMessage = "MaxTimePose is requied")]
        public int? MaxTimePose { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
        /*public CommentViewModel CommentVM { get; set; }*/
        public string AppUserId { get; set; }
        public string Email { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
