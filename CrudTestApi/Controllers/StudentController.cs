using CrudTestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CrudTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly CurdContext _context;
        public StudentController(CurdContext context)
        {
            _context = context;
        }
        //[HttpGet("StudentsListPage")]
        //public async Task<ActionResult<IEnumerable<StudentVM>>> GetStudentsList(int page = 1, int pageSize = 10, int? depId = null)
        //{
        //    ServiceResponse serviceResponse = new ServiceResponse();
        //    IQueryable<TblStudent> query = _context.TblStudents;          
        //    if (depId != null)
        //    {
        //        query = query.Where(s => s.DepId == depId);
        //    }
        //    var totalCount = await _context.TblStudents.CountAsync();
        //    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        //    var students = await _context.TblStudents
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    var studentvm = students.Select(student => new StudentVM
        //    {
        //        StudentId = student.StudentId,
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        DepId = student.DepId,
        //        QualifId = student.QualifId,
        //        Percentage = student.Percentage,
        //        DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
        //        QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
        //    }).ToList();


        //    var paginationMetadata = new
        //    {
        //        total_records = totalCount,
        //        current_page = page,
        //        total_pages = totalPages,
        //        next_page = page < totalPages ? page + 1 : (int?)null,
        //        prev_page = page > 1 ? page - 1 : (int?)null
        //    };
        //    if (studentvm.Count != 0)
        //    {
        //        serviceResponse.data = studentvm;
        //        serviceResponse.dataInfo = paginationMetadata;
        //        serviceResponse.isSuccess = true;   
        //    }
        //    //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

        //    return Ok(serviceResponse);
        //}
       

        [HttpGet("StudentsListPage")]
        public async Task<ActionResult<IEnumerable<StudentVM>>> GetStudentsList(int page = 1, int pageSize = 5, int? depId = null, int? qualifId = null, string firstName = null)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            var query = _context.TblStudents.AsQueryable();

          //  IQueryable<TblStudent> query = _context.TblStudents;
            if (depId != null)
            {
                query = query.Where(s => s.DepId == depId);
            }
            if (qualifId != null)
            {
                query = query.Where(s => s.QualifId == qualifId);
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(student => student.FirstName.Contains(firstName));
            }
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var students = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var studentvm = students.Select(student => new StudentVM
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DepId = student.DepId,
                QualifId = student.QualifId,
                Percentage = student.Percentage,
                DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
                QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
            }).ToList();

            var paginationMetadata = new
            {
                total_records = totalCount,
                current_page = page,
                total_pages = totalPages,
                next_page = page < totalPages ? page + 1 : (int?)null,
                prev_page = page > 1 ? page - 1 : (int?)null
            };
            if (studentvm.Count != 0)
            {
                serviceResponse.data = studentvm;
                serviceResponse.dataInfo = paginationMetadata;
                serviceResponse.isSuccess = true;
            }
            return Ok(serviceResponse);
        }

        [HttpGet("Students/{id}")]
        public async Task<ActionResult<StudentVM>> GetStudentById(int id)
        {
            var student = await _context.TblStudents.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentvm = new StudentVM
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DepId = student.DepId,
                QualifId = student.QualifId,
                Percentage = student.Percentage,
                DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
                QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
            };

            return studentvm;
        }

        //[HttpGet("GetDepQualif")]
        //public async Task<ActionResult<StudentVM>> GetDepQualif(int qualificationid)
        //{

        //    var student = await _context.TblStudents.Where(q=>q.QualifId ==qualificationid).ToListAsync();

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    //var studentvm = < StudentVM
        //    //foreach (var item in student)
        //    //{
        //    //    StudentId = student.StudentId,
        //    //    FirstName = student.FirstName,
        //    //    LastName = student.LastName,
        //    //    DepId = student.DepId,
        //    //    QualifId = student.QualifId,
        //    //    Percentage = student.Percentage,
        //    //    DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
        //    //    QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
        //    //}
        //    //var studentvm =  new StudentVM
        //    //{
        //    //    StudentId = student.StudentId,
        //    //    FirstName = student.FirstName,
        //    //    LastName = student.LastName,
        //    //    DepId = student.DepId,
        //    //    QualifId = student.QualifId,
        //    //    Percentage = student.Percentage,
        //    //    DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
        //    //    QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
        //    //}).ToList();

        //    //    studentvm;
        //    return student;
        //}
        [HttpGet("StudentsList")]
        public async Task<ActionResult<IEnumerable<StudentVM>>> GetStudentsList()
        {

            var students = await _context.TblStudents.ToListAsync();

            var studentvm = students.Select(student => new StudentVM
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DepId = student.DepId,
                QualifId = student.QualifId,
                Percentage = student.Percentage,
                DepartmentName = _context.TblDepartments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
                QualificationsName = _context.TblQualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
            }).ToList();

            return studentvm;
        }
        //[HttpGet("StudentsList")]
        //public async Task<ActionResult<IEnumerable<TblStudent>>> GetStudentsList()
        //{
        //    var students = await _context.TblStudents.ToListAsync();
        //    return Ok(students);
        //}
        [HttpGet("DepartmentsName")]
        public async Task<ActionResult<IEnumerable<TblStudent>>> GetDepartment()
        {
            var students = await _context.TblDepartments.ToListAsync();
            return Ok(students);
        }
        [HttpGet("QualificationsName")]
        public async Task<ActionResult<IEnumerable<TblStudent>>> GetQualification()
        {
            var students = await _context.TblQualifications.ToListAsync();
            return Ok(students);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TblStudent>> PostStudent(TblStudent student)
        {
            
            if ( student.StudentId ==0)
            {
                _context.TblStudents.Add(student);
                await _context.SaveChangesAsync();
            }
            return Ok(student);
        }
        [HttpPost("Update{id}")]
        public async Task<IActionResult> PostStudent(int id, TblStudent student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.TblStudents.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.TblStudents.Remove(student);
            await _context.SaveChangesAsync();

            return Ok();
        }
        private bool StudentExists(int id)
        {
            return _context.TblStudents.Any(e => e.StudentId == id);
        }
    }
}
