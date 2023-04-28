using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class AppUser : IdentityUser
    {

        /*[Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }*/
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Section { get; set; }
        public string? Phone { get; set; }

        // public string? ProfileImageUrl { get; set; }
        public string? NickName { get; set; }
        public string? State{ get; set; }
        public string? Icon { get; set; }
        public string? Account { get; set; }
        [ForeignKey("MainPose")]
        public int? MainPoseId { get; set; }
        public ICollection<MainPose> MainPoses { get; set; }
        [ForeignKey("Deposit")]
        public int? DepositId { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
    }
}
