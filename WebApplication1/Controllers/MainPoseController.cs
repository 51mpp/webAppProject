using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;


        public MainPoseController(IMainPoseRepository mainPoseRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _mainPoseRepository = mainPoseRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
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
        [HttpGet]
        public async Task<IActionResult> CreateMainPose()
        {   
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            AppUser user = await _userRepository.GetUserById(curUserId);
            var createMainPoseViewModel = new CreateMainPoseViewModel 
            { 
                AppUserId = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Account = user.Account
            };
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

                var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                AppUser user = await _userRepository.GetUserById(curUserId);
                if (user == null) return View("Error");
                var mainPose = new MainPose
                {
                    FirstName = mainPoseVM.FirstName,
                    LastName = mainPoseVM.LastName,
                    Phone = mainPoseVM.Phone,
                    AppUserId = mainPoseVM.AppUserId,
                    Icon = user.Icon,
                    Image = imageUrl,
                    Place = mainPoseVM.Place,
                    Account = mainPoseVM.Account,
                    MaxComment = mainPoseVM.MaxComment,
                    MaxTimePose = mainPoseVM.MaxTimePose,
                    CreatedTime = DateTime.Now,
                    Email = mainPoseVM.Email,
                    StatePost = "รอ"
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
        public async Task<IActionResult> GetMainPose()
        {
            IEnumerable<MainPose> mainPoses = await _mainPoseRepository.GetAll();
            foreach (MainPose mainPose in mainPoses)
            {
                mainPose.Comments = (ICollection<Comment>)await _mainPoseRepository.GetCommentsByMainPoseId(mainPose.Id);
            }
            return PartialView("_MainPosePartialView", (mainPoses));
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
                CreatedTime = post.CreatedTime,
                Email = post.Email,
                Icon = post.Icon,
                AppUserId = post.AppUserId,
                StatePost = post.StatePost

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
                Email = mainPoseVM.Email,
                Icon = mainPoseVM.Icon,
                AppUserId = mainPoseVM.AppUserId,
                StatePost = mainPoseVM.StatePost

            };
            _mainPoseRepository.Update(mainPose);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> StateMainPose(int id, string status)
        {
            if (!ModelState.IsValid) { return BadRequest("Invalid model state"); }
            var post = await _mainPoseRepository.GetByIdAsyncNoTracking(id);
            if (post == null) { return BadRequest("Can get MainPose"); }

            var mainPose = new MainPose
            {
                Id = id,
                FirstName = post.FirstName,
                LastName = post.LastName,
                Phone = post.Phone,
                Image = post.Image,
                Place = post.Place,
                Account = post.Account,
                MaxComment = post.MaxComment,
                CreatedTime = post.CreatedTime,
                MaxTimePose = post.MaxTimePose,
                LastModified = DateTime.Now,
                Email = post.Email,
                Icon = post.Icon,
                AppUserId = post.AppUserId,
                StatePost = status
            };
            _mainPoseRepository.Update(mainPose);
            return RedirectToAction("");
        }
        [HttpGet]
        public async Task<IActionResult> StatusMainPose(int id, string status)
        {
            string x = "";
            if (status == "รอ")
            {
                x = "blue";
            }
            else
            {
                x = "white";
            }
            return PartialView("_StatusPosePartialView", x);
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
        [HttpPost]
        public async Task<IActionResult> DeleteMainPose2(int mainPoseId)
        {
            var post = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _mainPoseRepository.Delete(post);
            return RedirectToAction("Index","Dashboard");
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int mainPoseId)
        {
            
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            int maxComment = (int)mainPose.MaxComment;
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(mainPoseId);
            return PartialView("_CommentPartialView", (comments, maxComment, mainPoseId, mainPose));
        }
        [HttpGet]
        public async Task<IActionResult> GetComments2(int mainPoseId)
        {

            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            int maxComment = (int)mainPose.MaxComment;
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(mainPoseId);
            return PartialView("_CommentPartialView2", (comments, maxComment, mainPoseId, mainPose));
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int mainPoseId, string CommentText, string FirstName, string LastName, IFormFile? image2, string email, bool confirm)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(mainPoseId);
            try
            {

            }catch (Exception ex)
            {
                return RedirectToAction("");
            }
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
                    Image = imageUrl,
                    Email = email,
                    Like = confirm
                };
                try {
                    await _mainPoseRepository.AddComment(comment);
                } catch (Exception ex) 
                {
                    return RedirectToAction("");
                }
                
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
        [HttpPost]
        public async Task<IActionResult> DeleteComment2(int commentId)
        {
            var post = await _mainPoseRepository.GetCommentEachByIdAsync(commentId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _mainPoseRepository.DeleteCommentEach(post);
            return RedirectToAction("Index","Dashboard");
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
            return Json(new { countComment = count });
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
        [HttpPost]
        public async Task<IActionResult> ConfirmComment(int commentId, string CommentText, string FirstName, string LastName, string? Image, string email, bool confirm, int mainPoseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }
            string comText = CommentText;
            string firstName = FirstName;
            string lastName = LastName;
            string emailText = email;
            string image = Image;
            var comment = new Comment
            {
                Id = commentId,
                FirstName = firstName,
                LastName = lastName,
                CommentText = comText,
                Email = emailText,
                Like = !confirm,
                MainPoseId = mainPoseId,
                Image = image
            };
            _mainPoseRepository.UpdateComment(comment);
            return Ok("Comment submitted successfully");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateComment(int commentId, string CommentText, string FirstName, string LastName, string? image, string email, bool confirm, int mainPoseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }
            string comText = CommentText;
            string firstName = FirstName;
            string lastName = LastName;
            string img = image;
            string emailText = email;
            var comment = new Comment
            {
                Id = commentId,
                FirstName = firstName,
                LastName = lastName,
                CommentText = comText,
                Image = img,
                Email = emailText,
                Like = confirm,
                MainPoseId = mainPoseId
            };
            _mainPoseRepository.UpdateComment(comment);
            return RedirectToAction("");
        }

    }
}
