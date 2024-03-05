using Microsoft.AspNetCore.Mvc;

namespace CurdView.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
