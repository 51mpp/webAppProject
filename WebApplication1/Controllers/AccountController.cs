using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            var response = new LoginVM { };
            return View(response);
        }
        [HttpPost]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Dashboard(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                //user found check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //password is correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginVM);
            }
            //user not found
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM { };
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            string imageUrl = null;
            if (registerVM.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(registerVM.Image);
                imageUrl = result.Url.ToString();
            }
            var newUser = new AppUser()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Section = registerVM.Section,
                Phone = registerVM.Phone,
                Account = registerVM.Account,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                NickName = registerVM.NickName,
                Icon = imageUrl
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("Login");
            }
            TempData["Error"] = "Format password should have: Use a combination of upper and lowercase letters, numbers, and symbols. Password at least 6-12 characters long.";
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}


