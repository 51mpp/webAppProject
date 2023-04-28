using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IMainPoseRepository
    {
        Task<IEnumerable<MainPose>> GetAll();
        Task<IEnumerable<Comment>> GetCommentsByMainPoseId(int mainPoseId);
        Task<Comment> GetCommentEachByIdAsync(int id);
        Task<MainPose> GetByIdAsync(int id);
        Task<MainPose> GetByIdAsyncNoTracking(int id);
        Task AddComment(Comment comment);
        Task AddPose(MainPose pose);
        bool Add(MainPose mainPose);
        bool Delete(MainPose mainPose);
        bool DeleteCommentEach(Comment comment);
        bool Update(MainPose mainPose);
        bool UpdateComment(Comment comment);
        bool Save();
    }
}
