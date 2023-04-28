using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository
{
    public class DepositRepository : IDepositRepository
    {
        private readonly ApplicationDbContext _context;

        public DepositRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Deposit deposit)
        {
            _context.Add(deposit);
            return Save();
        }

        public bool Delete(Deposit deposit)
        {
            _context.Remove(deposit);
            return Save();
        }
        public bool DeleteCommentEach(CommentDeposit commentDeposit)
        {
            _context.Remove(commentDeposit);
            return Save();
        }
        public async Task<IEnumerable<Deposit>> GetAll()
        {
            return await _context.Deposits.ToListAsync();
        }
        public async Task<IEnumerable<CommentDeposit>> GetCommentsByDepositId(int depositId)
        {
            return await _context.CommentDeposits.Where(c => c.DepositId == depositId).ToListAsync();
        }
        public async Task<CommentDeposit> GetCommentEachByIdAsync(int id)
        {
            return await _context.CommentDeposits.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Deposit> GetByIdAsync(int id)
        {
            return await _context.Deposits.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Deposit> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Deposits.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task AddPose(Deposit deposit)
        {
            await _context.Deposits.AddAsync(deposit);
            await _context.SaveChangesAsync();
        }
        public async Task AddComment(CommentDeposit comment)
        {
            await _context.CommentDeposits.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Deposit deposit)
        {
            _context.Update(deposit);
            return Save();
        }
        public bool UpdateComment(CommentDeposit commentDeposit)
        {
            _context.Update(commentDeposit);
            return Save();
        }
    }
}
