using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace new_diary.Controllers
{
    public class HomeController : Controller
    {
       
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
        public IActionResult Note()
        {
            return View();
        }
        [Authorize]
        public IActionResult Notebook()
        {
            return View();
        }
        


    }
}
