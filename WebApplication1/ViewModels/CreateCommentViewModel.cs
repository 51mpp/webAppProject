using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateCommentViewModel
    {

        public int Id { get; set; }
        public string CommentText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? MainPoseId { get; set; }
        public MainPose MainPose { get; set; }

    }
}
