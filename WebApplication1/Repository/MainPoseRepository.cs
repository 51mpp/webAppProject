using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository
{
    public class MainPoseRepository : IMainPoseRepository
    {
        private readonly ApplicationDbContext _context;

        public MainPoseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(MainPose mainPose)
        {
            _context.Add(mainPose);
            return Save();
        }

        public bool Delete(MainPose mainPose)
        {
            _context.Remove(mainPose);
            return Save();
        }
        public bool DeleteCommentEach(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }
        public async Task<IEnumerable<MainPose>> GetAll()
        {
            return await _context.MainPoses.ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByMainPoseId(int mainPoseId)
        {
            return await _context.Comments.Where(c => c.MainPoseId == mainPoseId).ToListAsync();
        }
        public async Task<Comment> GetCommentEachByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<MainPose> GetByIdAsync(int id)
        {
            return await _context.MainPoses.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<MainPose> GetByIdAsyncNoTracking(int id)
        {
            return await _context.MainPoses.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task AddPose(MainPose mainPose)
        {
            await _context.MainPoses.AddAsync(mainPose);
            await _context.SaveChangesAsync();
        }
        public async Task AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(MainPose mainPose)
        {
            _context.Update(mainPose);
            return Save();
        }
        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }


    }
}
