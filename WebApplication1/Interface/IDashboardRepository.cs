using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IDashboardRepository
    {
        Task<List<MainPose>> GetAllUserMainPose();
        Task<List<Deposit>> GetAllUserDeposit();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}