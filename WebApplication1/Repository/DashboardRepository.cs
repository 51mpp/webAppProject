using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Models;

namespace WebApplication1.Repository
{

    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<MainPose>> GetAllUserMainPose()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userMainPoses = _context.MainPoses.Where(r => r.AppUser.Id == curUser);
            return userMainPoses.ToList();
        }

    }
}