using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace WebApplication1.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Section { get; set; }
        public int? Phone { get; set; }
        public string? Icon { get; set; }
        public ICollection<MainPose> MainPoses { get; set; }
    }
}
