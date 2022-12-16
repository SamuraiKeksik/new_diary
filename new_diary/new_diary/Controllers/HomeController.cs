using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using new_diary.Models;

namespace new_diary.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _dbContext;
        public HomeController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
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
        


    }
}
