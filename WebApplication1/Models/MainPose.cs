using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebApplication1.Models
{
    public class MainPose
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Section { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        public string? Place { get; set; }
        public int? MaxComment { get; set; }
        public string? Account { get; set; }
        public int? MaxTimePose { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModified { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        [ForeignKey("AppUserIcon")]
        public string? AppUserIcon { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string? Email { get; set; }
        public string? StatePost { get; set; }

    }
}
