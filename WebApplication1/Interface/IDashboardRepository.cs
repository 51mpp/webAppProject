using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IDashboardRepository
    {
        Task<List<MainPose>> GetAllUserMainPose();
    }
}