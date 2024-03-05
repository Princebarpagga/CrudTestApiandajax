using MVCTest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MVCTest.Controllers
{
    public class EmployeeController : Controller
    {
        
        private CURDEntities db = new CURDEntities();

        //public ActionResult Index()
        //{

        //    List<Employee> employees = db.Employees.Include("Department").Include("Skill").ToList();
        //    return View(employees);
        //}

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            // Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SalarySortParam = sortOrder == "Salary" ? "salary_desc" : "Salary";
            ViewBag.DepartmentSortParam = sortOrder == "Department" ? "department_desc" : "Department";
            ViewBag.SkillSortParam = sortOrder == "Skill" ? "skill_desc" : "Skill";
            IQueryable<Employee> employees = db.Employees.Include("Department").Include("Skill");

            // Filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
           
           //searchString = searchString?.Trim(); 
           // if (!String.IsNullOrEmpty(searchString))
           // {
           //     searchString = searchString.ToLower();
           // }
          
            if (!String.IsNullOrEmpty(searchString) )
            {
                employees = employees.Where(e => e.EmployeeName.ToLower().Contains(searchString)
                                              || e.Skill.SkillName.ToLower().Contains(searchString)
                                              || e.Department.DepartmentName.ToLower().Contains(searchString)
                                              || e.Email.ToLower().Contains(searchString)
                                              );
            }
            //else
            //{
            //    ViewBag.NoDataFoundMessage = "No data found matching the search criteria.";
            //}

            // Paging
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.EmployeeName);
                    break;
                case "Salary":
                    employees = employees.OrderBy(e => e.Salary);
                    break;
                case "salary_desc":
                    employees = employees.OrderByDescending(e => e.Salary);
                    break;
                case "Department":
                    employees = employees.OrderBy(e => e.Department.DepartmentName);
                    break;
                case "department_desc":
                    employees = employees.OrderByDescending(e => e.Department.DepartmentName);
                    break;
                case "Skill":
                    employees = employees.OrderBy(e => e.Skill.SkillName);
                    break;
                case "skill_desc":
                    employees = employees.OrderByDescending(e => e.Skill.SkillName);
                    break;
                case "Email":
                    employees = employees.OrderBy(e => e.Email);
                    break;
                case "email_desc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;
                default:
                    employees = employees.OrderBy(e => e.EmployeeName);
                    break;
            }
            var pagedEmployees = employees.ToPagedList(pageNumber, pageSize);

            if (pagedEmployees.Count == 0 )
            {
                ViewBag.NoDataFoundMessage = "No data found matching the search criteria.";
            }
            // Apply paging
            //var pagedEmployees = db.Employees.ToList().ToPagedList(pageNumber, pageSize);
            //return View(pagedEmployees);
           // var pagedEmployees = employees.ToPagedList(pageNumber, pageSize);
            return View(pagedEmployees);

        }





        public ActionResult Create()
        {           
            var departments = db.Departments.ToList();            
            var departmentList = departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();           
            departmentList.Insert(0, new SelectListItem { Value = "", Text = "---------- Select Department ----------" });
                       
            ViewBag.DepartmentList = departmentList;
                     
            var skills = db.Skills.ToList();
           
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
        public ActionResult Create([Bind(Include = "EmpId,EmployeeName,Salary,Email,DepartmentId,SkillId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentList = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.SkillList = new SelectList(db.Skills, "SkillId", "SkillName", employee.SkillId);
            
            return PartialView("_Create", employee);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "EmpId,EmployeeName,Salary,DepartmentId,SkillId")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Employees.Add(employee);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    // If model state is not valid, return the partial view with validation errors
        //    var departments = db.Departments.ToList();
        //    var departmentList = departments.Select(d => new SelectListItem
        //    {
        //        Value = d.DepartmentId.ToString(),
        //        Text = d.DepartmentName
        //    }).ToList();
        //    departmentList.Insert(0, new SelectListItem { Value = "", Text = "---------- Select Department ----------" });

        //    ViewBag.DepartmentList = departmentList;

        //    var skills = db.Skills.ToList();

        //    var skillList = skills.Select(s => new SelectListItem
        //    {
        //        Value = s.SkillId.ToString(),
        //        Text = s.SkillName
        //    }).ToList();
        //    skillList.Insert(0, new SelectListItem { Value = "", Text = "------------- Select Skill --------------" });

        //    ViewBag.SkillList = skillList;           
        //    return PartialView("_Create", employee);
        //}

      
        public ActionResult Edit(int id)
        {
            
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartmentList = new SelectList(db.Departments.ToList(), "DepartmentId", "DepartmentName");

            
            ViewBag.SkillList = new SelectList(db.Skills.ToList(), "SkillId", "SkillName");
            return PartialView("_Edit", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Email))
            {
                ModelState.AddModelError("Email", "Email Reuired");
            }
            if (ModelState.IsValid)
            {
                
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            ViewBag.DepartmentList = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.SkillList = new SelectList(db.Skills, "SkillId", "SkillName", employee.SkillId);
            return PartialView("_Edit", employee);
        }
        public ActionResult Details(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", employee);
        }

        public ActionResult Delete(int id)
        {           
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

           
            return PartialView("_Delete", employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {           
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }                        
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}