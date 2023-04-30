namespace WebApplication1
{
    public class EditUserDashboardVM
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Section { get; set; }
        public string? Phone { get; set; }
        public string? Account { get; set; }
        public string? Icon { get; set; }
        public string? NickName { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
        public string? UrlImage { get; set; }
    }
}