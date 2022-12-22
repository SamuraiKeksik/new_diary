using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;
using new_diary.Models.ViewModels;

namespace new_diary.Controllers
{
    public class AdminController : Controller
    {
        public UserManager<MyUser> _userManager {get; set; }
        public AdminController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }



        public IActionResult Users() // Список пользователей
        {
            var users = _userManager.Users.ToList();    
            return View(users);
        }




        public IActionResult Login() => View(); //Аутентификация       
        /* [HttpPost]
        public async Task<IActionResult> Login(LoginUserForm loginForm)
        {
            var result = await _userManager.
        }*/



        public async Task<IActionResult> UserUpdate(string userId) //Изменение почты юзера
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Users");
            else            
                return View(user);    
        }        
        [HttpPost]
        public async Task<IActionResult> UserUpdate(string Id, string email) //Изменение почты юзера
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return RedirectToAction("Index", "Home");
            else
            {
                user.Email = email;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Users");
            }            
            
        }

       

        public IActionResult Create() => View(); //Регистрация
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserForm newUserForm) //Создание аккаунта - регистрация
        {
            if (ModelState.IsValid)
            {
                var newUser = new MyUser { UserName = newUserForm.Username, Email = newUserForm.Email };
                var result = await _userManager.CreateAsync(newUser, newUserForm.Password);
                if(result.Succeeded)                
                    return RedirectToAction("Index", "Home");                
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(newUserForm);            
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(string userId) //Удаление юзера
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Users");

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Users");
        }
    }
}
