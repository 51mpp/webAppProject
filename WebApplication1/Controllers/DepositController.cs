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
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositRepository _depositRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;


        public DepositController(IDepositRepository depositRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _depositRepository = depositRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index() // อันนี้ใช้ return view() ในหน้า club -----> controller 
        {
            IEnumerable<Deposit> deposits = await _depositRepository.GetAll();
            foreach (Deposit deposit in deposits)
            {
                deposit.CommentDeposits = (ICollection<CommentDeposit>)await _depositRepository.GetCommentsByDepositId(deposit.Id);
            }
            CreateDepositViewModel createMainPoseVM = new CreateDepositViewModel();
            CreateCommentDepositViewModel commentVM = new CreateCommentDepositViewModel();
            return View("Index", (deposits, createMainPoseVM, commentVM));
        }
        [HttpGet]
        public async Task<IActionResult> CreateDeposit()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            AppUser user = await _userRepository.GetUserById(curUserId);
            var createDepositViewModel = new CreateDepositViewModel 
            { 
                AppUserId = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Account = user.Account
            };
            return View(createDepositViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDeposit(CreateDepositViewModel depositVM)
        {
            if (ModelState.IsValid)
            {
                var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                AppUser user = await _userRepository.GetUserById(curUserId);
                var deposit = new Deposit
                {
                    FirstName = depositVM.FirstName,
                    LastName = depositVM.LastName,
                    Icon = user.Icon,
                    Phone = depositVM.Phone,
                    Place = depositVM.Place,
                    Food = depositVM.Food,
                    PlaceDeliver = depositVM.PlaceDeliver,
                    MaxTimePose = depositVM.MaxTimePose,
                    AppUserId = depositVM.AppUserId,
                    CreatedTime = DateTime.Now,
                    Email = depositVM.Email,
                    StatePost = "รอ"

                };

                _depositRepository.Add(deposit);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(depositVM);
        }
        [HttpGet]
        public async Task<IActionResult> EditDeposit(int id)
        {
            var post = await _depositRepository.GetByIdAsync(id);
            if (post == null) { return View("Error"); }
            var depositVM = new EditDepositViewModel
            {
                Id = id,
                FirstName = post.FirstName,
                LastName = post.LastName,
                Phone = post.Phone,
                Place = post.Place,
                MaxTimePose = post.MaxTimePose,
                CreatedTime = post.CreatedTime,
                Food = post.Food,
                PlaceDeliver = post.PlaceDeliver,
                Icon = post.Icon,
                Email = post.Email,
                AppUserId = post.AppUserId,
                StatePost = post.StatePost

            };
            return View(depositVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditDeposit(int id, EditDepositViewModel depositVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fail to edit EditDepositPose");
                return View("EditDeposit", depositVM);
            }
            var post = await _depositRepository.GetByIdAsyncNoTracking(id);
            if (post == null)
            {
                return View("Error");
            }
            var deposit = new Deposit
            {
                Id = id,
                FirstName = depositVM.FirstName,
                LastName = depositVM.LastName,
                Phone = depositVM.Phone,
                Place = depositVM.Place,
                MaxTimePose = depositVM.MaxTimePose,
                CreatedTime = depositVM.CreatedTime,
                Food = depositVM.Food,
                PlaceDeliver = depositVM.PlaceDeliver,
                Icon = depositVM.Icon,
                Email = depositVM.Email,
                AppUserId = depositVM.AppUserId,
                StatePost = depositVM.StatePost,
                LastModified = DateTime.Now,

            };
            _depositRepository.Update(deposit);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> StateMainPose(int id, string status)
        {
            if (!ModelState.IsValid) { return BadRequest("Invalid model state"); }
            var post = await _depositRepository.GetByIdAsyncNoTracking(id);
            if (post == null) { return BadRequest("Can get MainPose"); }

            var deposit = new Deposit
            {
                Id = id,
                FirstName = post.FirstName,
                LastName = post.LastName,
                Phone = post.Phone,
                Place = post.Place,
                Food = post.Food,
                PlaceDeliver = post.PlaceDeliver,
                MaxTimePose = post.MaxTimePose,
                CreatedTime = post.CreatedTime,
                Email = post.Email,
                AppUserId = post.AppUserId,
                StatePost = status,
                LastModified = DateTime.Now,
                Icon = post.Icon
            };
            _depositRepository.Update(deposit);
            return RedirectToAction("");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDeposit(int depositId)
        {
            var post = await _depositRepository.GetByIdAsync(depositId);
            if (post == null) { return View("Error"); }

            _depositRepository.Delete(post);
            return RedirectToAction("");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDeposit2(int depositId)
        {
            var post = await _depositRepository.GetByIdAsync(depositId);
            if (post == null) { return View("Error"); }

            _depositRepository.Delete(post);
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int depositId)
        {
            Deposit deposit = await _depositRepository.GetByIdAsync(depositId);
            IEnumerable<CommentDeposit> commentDeposits = await _depositRepository.GetCommentsByDepositId(depositId);
            return PartialView("_CommentDepositPartialView", (commentDeposits, deposit));
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int depositId, string CommentText, string FirstName, string LastName, IFormFile? image, string email, bool confirm)
        {
            Deposit deposits = await _depositRepository.GetByIdAsync(depositId);
            if (ModelState.IsValid)
            {
                string comText = "";
                string firstName = "";
                string lastName = "";
                string imageUrl = null;
                if (image != null)
                {
                    var result = await _photoService.AddPhotoCommentDepositAsync(image);
                    imageUrl = result.Url.ToString();
                }
                if (CommentText != null)
                {
                    comText = CommentText;
                    firstName = FirstName;
                    lastName = LastName;
                }
                CommentDeposit comment = new CommentDeposit
                {
                    DepositId = depositId,
                    CommentText = comText,
                    FirstName = firstName,
                    LastName = lastName,
                    Image = imageUrl,
                    Email = email,
                    Like = confirm
                };
                try
                {
                    await _depositRepository.AddComment(comment);
                }
                catch (Exception ex)
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
        public async Task<IActionResult> DeleteComment(int depositId)
        {
            var post = await _depositRepository.GetCommentEachByIdAsync(depositId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _depositRepository.DeleteCommentEach(post);
            return RedirectToAction("");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment2(int depositId)
        {
            var post = await _depositRepository.GetCommentEachByIdAsync(depositId);
            if (post == null) { return View("Error"); }
            if (!string.IsNullOrEmpty(post.Image))
            {
                _ = _photoService.DeletePhotoAsync(post.Image); // ไม่สนใจรีเทิน
            }
            _depositRepository.DeleteCommentEach(post);
            return RedirectToAction("Index","Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCommentByAjax(int? depositId, string CommentText, string FirstName, string LastName)
        {
            if (ModelState.IsValid)
            {
                string comText = CommentText;
                string firstName = FirstName;
                string lastName = LastName;
                CommentDeposit comment = new CommentDeposit
                {
                    DepositId = depositId,
                    CommentText = comText,
                    FirstName = firstName,
                    LastName = lastName
                };
                await _depositRepository.AddComment(comment);
                return Json(new { success = true, comment = comment });
            }
            else
            {
                ModelState.AddModelError("", "Comment Failed");

            }
            return Json(new { success = false, errors = ModelState });
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentDeposit(int depositId)
        {
            Deposit deposit = await _depositRepository.GetByIdAsync(depositId);
            IEnumerable<CommentDeposit> comments = await _depositRepository.GetCommentsByDepositId(depositId);
            return PartialView("_CommentDepositPartialView", (comments, deposit));
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentDeposit2(int depositId)
        {
            Deposit deposit = await _depositRepository.GetByIdAsync(depositId);
            IEnumerable<CommentDeposit> comments = await _depositRepository.GetCommentsByDepositId(depositId);
            return PartialView("_CommentDepositPartialView2", (comments, deposit));
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmComment(int commentDepositId, int depositId, string CommentText, string FirstName, string LastName, string? image, string email, bool confirm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }
            string comText = CommentText;
            string firstName = FirstName;
            string lastName = LastName;
            string emailText = email;
            string img = image;
            var commentDeposit = new CommentDeposit
            {
                Id = commentDepositId,
                FirstName = firstName,
                LastName = lastName,
                CommentText = comText,
                Email = emailText,
                Like = !confirm,
                DepositId = depositId,
                Image = img
            };
            _depositRepository.UpdateComment(commentDeposit);
            return Ok("Comment submitted successfully");
        }

    }
}
