using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateMainPoseViewModel
    {
        
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Section { get; set; }
        public int? Phone { get; set; }
        public string? Image { get; set; }
        public string? Place { get; set; }
        public int? AppUserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
