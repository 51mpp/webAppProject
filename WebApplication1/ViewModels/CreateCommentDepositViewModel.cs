using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateCommentDepositViewModel
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public int? DepositId { get; set; }
        public Deposit Deposit { get; set; }
    }
}
