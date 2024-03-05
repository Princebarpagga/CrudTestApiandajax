using Microsoft.AspNetCore.Mvc;

namespace AjaxView.Controllers
{
    public class userController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
