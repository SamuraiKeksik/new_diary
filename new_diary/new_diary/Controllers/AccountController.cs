using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;
using new_diary.Models.ViewModels;

namespace new_diary.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<MyUser> _signInManager;
        UserManager<MyUser> _userManager;
        public AccountController(SignInManager<MyUser> signInManager, UserManager<MyUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string? returnUrl) //Аутентификация юзера
        {
            var loginModel = new LoginModel { ReturnUrl = returnUrl ?? "/" };
            return View(loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if(ModelState.IsValid)
            {                
                var user = await _userManager.FindByEmailAsync(login.Email);
                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Main", "Home");
                }
                ModelState.AddModelError(nameof(login.Email), "Invalid Email or password");

            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
