using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class DashboardVM
    {
        public List<MainPose> MainPoses { get; set; }
        public List<Deposit> Deposites { get; set; }
    }
}