namespace WebApplication1
{
    public class EditUserDashboardVM
    {
        public string Id { get; set; }
        public int? Section { get; set; }
        public string? Phone { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile Image { get; set; }
    }
}