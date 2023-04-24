using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
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
            CreateCommentViewModel commentVM = new CreateCommentViewModel();
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
                    MaxTimePose = mainPoseVM.MaxTimePose,
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
                MaxTimePose = post.MaxTimePose,
                CreatedTime = post.CreatedTime

            };
            return View(mainPoseVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditMainPose(int id, EditMainPoseViewModel mainPoseVM)
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
            else
            {
                imageUrl = mainPoseVM.URLImage;
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
                MaxTimePose = mainPoseVM.MaxTimePose,
                LastModified = DateTime.Now,

            };
            _mainPoseRepository.Update(mainPose);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMainPose(int mainPoseId)
        {
            var post = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _mainPoseRepository.Delete(post);
            return RedirectToAction("");
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int mainPoseId)
        {
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(mainPoseId);
            return PartialView("_CommentPartialView", comments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int mainPoseId, string CommentText, string FirstName, string LastName,IFormFile? image2)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            
            if (ModelState.IsValid)
            {
                string imageUrl = "";
                if (image2 != null)
                {
                    var result = await _photoService.AddPhotoCommentAsync(image2);
                    imageUrl = result.Url.ToString();
                }
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
                    LastName = lastName,
                    Image = imageUrl
                };
                await _mainPoseRepository.AddComment(comment);
                return RedirectToAction("");
            }
            else
            {
                ModelState.AddModelError("", "Comment Failed");
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var post = await _mainPoseRepository.GetCommentEachByIdAsync(commentId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _mainPoseRepository.DeleteCommentEach(post);
            return RedirectToAction("");
        }
        [HttpGet]
        public async Task<IActionResult> CountComments(int mainPoseId)
        {
            MainPose mainPoses = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(mainPoseId);
            int count = comments.Count();
            int max = (int)mainPoses.MaxComment;
            /*ฝากแล้ว @item.Comments.Count / @item.MaxComment คน*/
            string html = $"<span id='count-{mainPoseId}-comments'>ฝากแล้ว {count}  / {max} คน</span>";
            return Content(html);
        }
        [HttpGet]
        public async Task<IActionResult> CountComments2(int mainPoseId)
        {
            MainPose mainPoses = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(mainPoseId);
            int count = comments.Count();
            return Json(new {countComment = count });
        }
        [HttpGet]
        public IActionResult GetDate(DateTime CreatedTime)
        {
            var x = DateTime.Now - CreatedTime;
            string y = "";
            if (x.Minutes <= 0 && x.Hours <= 0 && x.Days <= 0)
            {
                y = "โพสต์เมื่อซักครู่";
            }
            else if (x.Hours <= 0 && x.Days <= 0)
            {
                y = string.Format("โพสต์เมื่อ {0} นาทีที่แล้ว", x.Minutes);
            }
            else if (x.Hours > 0 && x.Days <= 0)
            {
                y = string.Format("โพสต์เมื่อ {0} ชั่วโมงที่แล้ว", x.Hours);
            }
            else if (x.Days > 0 && x.Days <= 7)
            {
                y = string.Format("โพสต์เมื่อ {0} วันที่แล้ว ", x.Days);
            }
            else if (x.Days <= 365)
            {
                y = CreatedTime.ToString("โพสต์เมื่อ dd MMMM เวลา HH:mm น.", new CultureInfo("th-TH"));
            }
            else
            {
                y = CreatedTime.ToString("โพสต์เมื่อ dd MMMM yyyy เวลา HH:mm น.", new
                CultureInfo("th-TH"));
            }
            return Json(new { formattedDate = y });
        }
        public async Task<IActionResult> Detail(int id)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(id);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(id);

            return View("Detail", (mainPose, comments));

        }
        [HttpPost]
        public async Task<IActionResult> CreateCommentByAjax(int mainPoseId, string CommentText, string FirstName, string LastName)
        {
            if (ModelState.IsValid)
            {
                string comText = CommentText;
                string firstName = FirstName;
                string lastName = LastName;
                Comment comment = new Comment
                {
                    MainPoseId = mainPoseId,
                    CommentText = comText,
                    FirstName = firstName,
                    LastName = lastName
                };
                await _mainPoseRepository.AddComment(comment);
                return Json(new { success = true, comment = comment });
            }
            else
            {
                ModelState.AddModelError("", "Comment Failed");

            }
            return Json(new { success = false, errors = ModelState });
        }

    }
}
