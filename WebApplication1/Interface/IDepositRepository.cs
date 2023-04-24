using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IDepositRepository
    {
        Task<IEnumerable<Deposit>> GetAll();
        Task<IEnumerable<CommentDeposit>> GetCommentsByDepositId(int depositId);
        Task<Deposit> GetByIdAsync(int id);
        Task<Deposit> GetByIdAsyncNoTracking(int id);
        Task AddComment(CommentDeposit commentDeposit);
        Task AddPose(Deposit deposit);
        bool Add(Deposit deposit);
        bool Delete(Deposit deposit);
        bool Update(Deposit deposit);
        bool Save();
    }
}
