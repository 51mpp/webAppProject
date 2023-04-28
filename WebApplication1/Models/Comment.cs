using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        [ForeignKey("MainPose")]
        public int? MainPoseId { get; set; }
        public MainPose MainPose { get; set; }

        public string? Email { get; set; }
        public bool? Like { get; set; }
    }
}
