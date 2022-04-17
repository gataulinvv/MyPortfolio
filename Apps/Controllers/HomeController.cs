using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apps.MVCApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
