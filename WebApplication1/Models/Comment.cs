using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        [ForeignKey("MainPose")]
        public int? MainPoseId { get; set; }
        public MainPose MainPose { get; set; }
    }
}
