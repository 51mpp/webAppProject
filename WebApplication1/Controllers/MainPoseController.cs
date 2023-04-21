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
        [HttpGet]
        public async Task<IActionResult> Index() // อันนี้ใช้ return view() ในหน้า club -----> controller 
        {
            IEnumerable<MainPose> mainPoses = await _mainPoseRepository.GetAll();
            foreach (MainPose mainPose in mainPoses)
            {
                mainPose.Comments = (ICollection<Comment>)await _mainPoseRepository.GetCommentsByMainPoseId(mainPose.Id);
            }
            CreateMainPoseViewModel createMainPoseVM = new CreateMainPoseViewModel();
            CommentViewModel commentVM = new CommentViewModel();
            return View("Index", (mainPoses, createMainPoseVM, commentVM));
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
                    Account = mainPoseVM.Account,
                    MaxComment = mainPoseVM.MaxComment,
                    CreatedTime = DateTime.Now,
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
        [HttpGet]
        public async Task<IActionResult> EditMainPose(int id)
        {
            var post = await _mainPoseRepository.GetByIdAsync(id);
            if (post == null) { return View("Error"); }
            var mainPoseVM = new EditMainPoseViewModel
            {
                FirstName = post.FirstName,
                LastName = post.LastName,
                Phone = post.Phone,
                URLImage = post.Image,
                Place = post.Place,
                Account = post.Account,
                MaxComment = post.MaxComment,
                CreatedTime = post.CreatedTime

            };
            return View(mainPoseVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditMainPose(int id,EditMainPoseViewModel mainPoseVM) 
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Fail to edit mainPose");
                return View("EditMainPose", mainPoseVM); 
            }
            var post = await _mainPoseRepository.GetByIdAsyncNoTracking(id);
            if (post == null)
            {
                return View("Error");
            }
            string imageUrl = null;
            if (mainPoseVM.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(mainPoseVM.Image);
                imageUrl = result.Url.ToString();
            }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            var mainPose = new MainPose
            {
                Id = id,
                FirstName = mainPoseVM.FirstName,
                LastName = mainPoseVM.LastName,
                Phone = mainPoseVM.Phone,
                Image = imageUrl,
                Place = mainPoseVM.Place,
                Account = mainPoseVM.Account,
                MaxComment = mainPoseVM.MaxComment,
                CreatedTime = mainPoseVM.CreatedTime,
                LastModified = DateTime.Now,
                
            };
            _mainPoseRepository.Update(mainPose);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMainPose(int mainPoseId)
        {
            var post = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (post == null) {  return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _mainPoseRepository.Delete(post);
            return RedirectToAction("");
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int mainPoseId, string CommentText, string FirstName, string LastName)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (ModelState.IsValid)
            {
                string comText = "";
                string firstName = "";
                string lastName = "";
                if (CommentText != null)
                {
                    comText = CommentText;
                    firstName = FirstName;
                    lastName = LastName;

                }
                Comment comment = new Comment
                {
                    MainPoseId = mainPoseId,
                    CommentText = comText,
                    FirstName = firstName,
                    LastName = lastName
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
            /*return RedirectToAction("");*/
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(id);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(id);

            return View("Detail", (mainPose, comments));

        }
    }
}
