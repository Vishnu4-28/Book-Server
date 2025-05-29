using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Server.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
