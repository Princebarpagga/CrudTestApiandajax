using codeFirstCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace codeFirstCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _context.Employees.Include("Department").Include("Skill").ToList();
            //var employees = _context.Employees.ToList(); 
            return View(employees);
        }
       

        [HttpGet]
        public IActionResult LoadData()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Skill)
                .ToList();

            return Json(new { data = employees });
        }
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            var departmentList = departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
            departmentList.Insert(0, new SelectListItem { Value = "", Text = "---------- Select Department ----------" });

            ViewBag.DepartmentList = departmentList;

            var skills = _context.Skills.ToList();

            var skillList = skills.Select(s => new SelectListItem
            {
                Value = s.SkillId.ToString(),
                Text = s.SkillName
            }).ToList();
            skillList.Insert(0, new SelectListItem { Value = "", Text = "------------- Select Skill --------------" });

            ViewBag.SkillList = skillList;
            return PartialView("_Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
          //  if (ModelState.IsValid)
                if (employee!= null)
                {
                    _context.Add(employee);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.SkillList = new SelectList(_context.Skills, "SkillId", "SkillName", employee.SkillId);

            return PartialView("_Create", employee);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.SkillList = new SelectList(_context.Skills, "SkillId", "SkillName", employee.SkillId);
            return PartialView("_Edit", employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {


            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Update(employee);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmpId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            // }
            return PartialView("_Edit", employee);
        }


        public ActionResult Details(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return PartialView("_Details", employee);
        }

        //public ActionResult Delete(int id)
        //{
        //    Employee employee = _context.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView("_Delete", employee);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = _context.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Employees.Remove(employee);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public IActionResult DeleteMultiple(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No employee IDs provided.");
            }

            try
            {
                var employeesToDelete = _context.Employees.Where(e => ids.Contains(e.EmpId)).ToList();
                if (employeesToDelete == null || employeesToDelete.Count == 0)
                {
                    return NotFound("Employees not found.");
                }
                _context.Employees.RemoveRange(employeesToDelete);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occurred while deleting employees: {ex.Message}");
            }
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }

    }
}
