using Angulartestapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxView.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly AngularTestDbContext _context;
        //public EmployeeController(AngularTestDbContext context)
        //{
        //    _context = context;
        //}
        public IActionResult Index()
        {
            return View();
        }
       

        
       
    }

}
