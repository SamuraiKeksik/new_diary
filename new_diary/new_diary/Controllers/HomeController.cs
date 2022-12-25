using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using new_diary.Models;
using new_diary.Models.ViewModels;
using System.Security.Claims;

namespace new_diary.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _dbContext;
        UserManager<MyUser> _userManager;
        public HomeController(ApplicationContext dbContext, UserManager<MyUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private MainModel GetMainModel()   //создание новой MainModel
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newModel = new MainModel();
            newModel.notebooks = _dbContext.Notebooks.Where(x => x.UserId.ToString() == userId).ToArray();
            newModel.notes = _dbContext.Notes.Where(x => x.UserId.ToString() == userId).ToArray();
            return newModel;
        }


        // To convert the Byte Array to the author Image 
        public FileContentResult GetUserPicture()   //Получение картинки с сервера
        {
          var byteArray = _userManager.FindByNameAsync(User.Identity.Name).Result.UserPicture;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserPicture(IFormFile userPicture) //Загрузка картинки на сервер
        {
            if (userPicture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    userPicture.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    // string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data

                    var myUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    myUser.UserPicture = fileBytes;
                    await _userManager.UpdateAsync(myUser);
                }
               
            }
            return RedirectToAction("Main");            
            
        }

        public IActionResult Index()
        {            
            return View();
        }

        [Authorize]
        public IActionResult Main()
        {
            var newModel = GetMainModel();
            return View(newModel);
        }
        [Authorize]
        public IActionResult Notes()
        {
            var newModel = GetMainModel();
            return View(newModel);
        }
        [Authorize]
        public IActionResult Notebooks()
        {
            return View();
        }


        [Authorize]
        public void NoteCreation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newNote = new Note {UserId = new Guid(userId)};
        }

    }
}
