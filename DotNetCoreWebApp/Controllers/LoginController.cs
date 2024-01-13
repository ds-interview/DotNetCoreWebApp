using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApp.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
