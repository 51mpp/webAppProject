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
        public int? Section { get; set; }
        public int? Phone { get; set; }
        public IFormFile? Image { get; set; }
        
        public string? Place { get; set; }
        public int? MaxComment { get; set; }
        public string? Account { get; set; }
        /*public CommentViewModel CommentVM { get; set; }*/
        //public int? AppUserId { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
