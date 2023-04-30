using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class  DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;
        private readonly IMainPoseRepository _mainPoseRepository;


        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService, IUserRepository userRepository, IMainPoseRepository mainPoseRepository)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _userRepository = userRepository;
            _mainPoseRepository = mainPoseRepository;

        }
        private void MapUserEdit(AppUser user, EditUserDashboardVM editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Section = editVM.Section;
            user.Phone = editVM.Phone;
            user.Account = editVM.Account;
            user.Icon = photoResult.Url.ToString();
            user.NickName = editVM.NickName;
            user.State = editVM.State;
            user.Account = editVM.Account;
        }
        private void MapUserEdit2(AppUser user, EditUserDashboardVM editVM)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Section = editVM.Section;
            user.Phone = editVM.Phone;
            user.Icon = editVM.UrlImage;
            user.NickName = editVM.NickName;
            user.State = editVM.State;
            user.Account = editVM.Account;
        }
        public async Task<IActionResult> Index()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            AppUser user = await _userRepository.GetUserById(curUserId);
            var userMainPoses = await _dashboardRepository.GetAllUserMainPose();
            var userDeposits = await _dashboardRepository.GetAllUserDeposit();
            var dashboardVM = new DashboardVM()
            {
                MainPoses = userMainPoses,
                Deposites = userDeposits
            };
            return View("Index", (dashboardVM, user));
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if(user == null) return View("Error");
            var editUserVM = new EditUserDashboardVM()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Section = user.Section,
                Phone = user.Phone,
                Account = user.Account,
                Icon = user.Icon,
                NickName = user.NickName,
                State = user.State,
                UrlImage = user.Icon
            };
            return View(editUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardVM editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if(user.Icon == "" || user.Icon == null)
            {

                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                
                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                if (editVM.Image != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(user.Icon);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(editVM);
                    }
                    var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                    MapUserEdit(user, editVM, photoResult);
                }
                else
                {
                    MapUserEdit2(user, editVM);
                }
                


                _dashboardRepository.Update(user);
                
                return RedirectToAction("Index");
            }
        }


    }
}