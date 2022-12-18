using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;
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

        
        public IActionResult Index()
        {
            var notebooks = _dbContext.Notebooks.ToList();
            return View(notebooks);
        }

        [Authorize]
        public IActionResult Main()
        {
            return View();
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
