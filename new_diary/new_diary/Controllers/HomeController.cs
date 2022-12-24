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

        // To convert the Byte Array to the author Image - возврат картинки юзера
        public FileContentResult GetUserPicture(string id)
        {
            byte[] byteArray = _userManager.FindByIdAsync(id).Result.UserPicture;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [Authorize]
        public IActionResult Main()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newModel = new MainModel();
            newModel.notebooks = _dbContext.Notebooks.Where(x => x.UserId.ToString() == userId).ToArray();
            newModel.notes = _dbContext.Notes.Where(x => x.UserId.ToString() == userId).ToArray();
            return View(newModel);
        }
        [Authorize]
        public IActionResult Notes()
        {
            return View();
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
