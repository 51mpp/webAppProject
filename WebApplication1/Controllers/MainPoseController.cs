using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class MainPoseController : Controller
    {
        private readonly IMainPoseRepository _mainPoseRepository;


        public MainPoseController(IMainPoseRepository mainPoseRepository)
        {
            _mainPoseRepository = mainPoseRepository;


        }
        public async Task<IActionResult> Index() // อันนี้ใช้ return view() ในหน้า club -----> controller 
        {
            IEnumerable<MainPose> mainPoses = await _mainPoseRepository.GetAll();
            foreach (MainPose mainPose in mainPoses)
            {
                mainPose.Comments = (ICollection<Comment>)await _mainPoseRepository.GetCommentsByMainPoseId(mainPose.Id);
            }
            return View(mainPoses);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateComment(int mainPoseId, string commentText)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (mainPose == null)
            {
                return NotFound();
            }
            Comment comment = new Comment
            {
                MainPoseId = mainPoseId,
                CommentText = commentText
            };
            await _mainPoseRepository.AddComment(comment);
            return RedirectToAction("");
        }
        public async Task<IActionResult> Detail(int id)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(id);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(id);
           
            return View("Detail", (mainPose, comments));

        }

    }
}
