using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CommentDeposit
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Deposit")]
        public int? DepositId { get; set; }
        public Deposit Deposit { get; set; }

        public string? Email { get; set; }
        public bool? Like { get; set; }
    }
}
