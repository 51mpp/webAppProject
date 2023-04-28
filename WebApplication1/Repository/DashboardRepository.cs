using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Deposit>> GetAllUserDeposit()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userDeposits = _context.Deposits.Where(r => r.AppUser.Id == curUser);
            return userDeposits.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved> 0 ? true : false;
        }

    }
}