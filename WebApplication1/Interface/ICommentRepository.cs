using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByMainPoseId(int mainPoseId);
    }
}
