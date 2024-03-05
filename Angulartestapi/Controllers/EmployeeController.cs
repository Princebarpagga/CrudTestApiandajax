using Angulartestapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angulartestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AngularTestDbContext _context;
       

        public EmployeeController(AngularTestDbContext context)
        {
            _context = context;
           
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            // return await _context.Employees.ToListAsync();
            //var employees = await _context.Employees.ToListAsync();
            //var department = await _context.Departments.ToListAsync();
            //var skill = await _context.Skills.ToListAsync();

            //return Ok();
            var employees = await _context.Employees.ToListAsync();

            var employeeDtos = employees.Select(employee => new EmployeeDto
            {
                EmpId = employee.EmpId,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                SkillId = employee.SkillId,
                Email = employee.Email,
                DepartmentName = _context.Departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId)?.DepartmentName,
                SkillName = _context.Skills.FirstOrDefault(s => s.SkillId == employee.SkillId)?.SkillName
            }).ToList();

            return employeeDtos;
        }


        [HttpGet("DepartmentName")] 
        public async Task<ActionResult<IEnumerable<Employee>>>GetDepartment()
        {
            var employees = await _context.Departments.ToListAsync();
            return Ok(employees);
        }
        [HttpGet("SkillName")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSkill()
        {
            var employees = await _context.Skills.ToListAsync();
            return Ok(employees);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }
        [HttpPut("Update{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
