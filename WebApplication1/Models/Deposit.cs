using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public string? Place { get; set; }
        public string? Icon { get; set; }
        public string? Food { get; set; }
        public string? PlaceDeliver { get; set; }
        public int? MaxTimePose { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<CommentDeposit> CommentDeposits { get; set; }

        public string? Email { get; set; }
        public string? StatePost { get; set; }
    }
}
