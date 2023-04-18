using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Interface;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class MainPoseController : Controller
    {
        private readonly IMainPoseRepository _mainPoseRepository;
        private readonly IPhotoService _photoService;


        public MainPoseController(IMainPoseRepository mainPoseRepository, IPhotoService photoService)
        {
            _mainPoseRepository = mainPoseRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index() // อันนี้ใช้ return view() ในหน้า club -----> controller 
        {
            IEnumerable<MainPose> mainPoses = await _mainPoseRepository.GetAll();
            foreach (MainPose mainPose in mainPoses)
            {
                mainPose.Comments = (ICollection<Comment>)await _mainPoseRepository.GetCommentsByMainPoseId(mainPose.Id);
            }
            CreateMainPoseViewModel viewModel = new CreateMainPoseViewModel();
            return View("Index", (mainPoses, viewModel));
        }
        public IActionResult CreateMainPose()
        {

            var createMainPoseViewModel = new CreateMainPoseViewModel { };
            return View(createMainPoseViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMainPose(CreateMainPoseViewModel mainPoseVM)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = null;
                if (mainPoseVM.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(mainPoseVM.Image);
                    imageUrl = result.Url.ToString();
                }
                var mainPose = new MainPose
                {
                    FirstName = mainPoseVM.FirstName,
                    LastName = mainPoseVM.LastName,
                    Phone = mainPoseVM.Phone,
                    Image = imageUrl,
                    Place = mainPoseVM.Place,
                };

                _mainPoseRepository.Add(mainPose);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(mainPoseVM);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int mainPoseId, string commentText)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (ModelState.IsValid)
            {
                string comText = "";
                if (commentText != null)
                {
                    comText = commentText;
                }
                Comment comment = new Comment
                {
                    MainPoseId = mainPoseId,
                    CommentText = comText
                };
                await _mainPoseRepository.AddComment(comment);
                return RedirectToAction("");
            }
            else
            {
                ModelState.AddModelError("", "Comment Failed");
            }
            /*if (mainPose == null)
            {
                return NotFound();
            }*/
            /* Comment comment = new Comment
             {
                 MainPoseId = mainPoseId,
                 CommentText = commentText
             };
             await _mainPoseRepository.AddComment(comment);
             return RedirectToAction("");*/
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
