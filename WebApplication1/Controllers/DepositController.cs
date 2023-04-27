﻿using Microsoft.AspNetCore.Mvc;
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


        public DepositController(IDepositRepository depositRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _depositRepository = depositRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult CreateDeposit()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createDepositViewModel = new CreateDepositViewModel { AppUserId = curUserId  };
            return View(createDepositViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDeposit(CreateDepositViewModel depositVM)
        {
            if (ModelState.IsValid)
            {
                var deposit = new Deposit
                {
                    FirstName = depositVM.FirstName,
                    LastName = depositVM.LastName,
                    Phone = depositVM.Phone,
                    Place = depositVM.Place,
                    Food = depositVM.Food,
                    PlaceDeliver = depositVM.PlaceDeliver,
                    MaxTimePose = depositVM.MaxTimePose,
                    AppUserId = depositVM.AppUserId,
                    CreatedTime = DateTime.Now
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
                FirstName = post.FirstName,
                LastName = post.LastName,
                Phone = post.Phone,
                Place = post.Place,
                Food = post.Food,
                PlaceDeliver = post.PlaceDeliver,
                MaxTimePose = post.MaxTimePose,
                CreatedTime = post.CreatedTime

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
                Food = depositVM.Food,
                PlaceDeliver = depositVM.PlaceDeliver,
                CreatedTime = depositVM.CreatedTime,
                MaxTimePose = depositVM.MaxTimePose,
                LastModified = DateTime.Now,

            };
            _depositRepository.Update(deposit);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDeposit(int depositId)
        {
            var post = await _depositRepository.GetByIdAsync(depositId);
            if (post == null) { return View("Error"); }

            _depositRepository.Delete(post);
            return RedirectToAction("");
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int depositId)
        {
            IEnumerable<CommentDeposit> commentDeposits = await _depositRepository.GetCommentsByDepositId(depositId);
            return PartialView("_CommentDepositPartialView", commentDeposits);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int depositId, string CommentText, string FirstName, string LastName, IFormFile? image)
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
                    var result = await _photoService.AddPhotoCommentAsync(image);
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
                    Image = imageUrl
                };
                await _depositRepository.AddComment(comment);
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
            IEnumerable<CommentDeposit> comments = await _depositRepository.GetCommentsByDepositId(depositId);
            return PartialView("_CommentDepositPartialView", comments);
        }

    }
}
