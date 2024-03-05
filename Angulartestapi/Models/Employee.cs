using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Angulartestapi.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public int? SkillId { get; set; }
        public string Email { get; set; }
        //public Department Department { get; set; }
        //public Skill Skill { get; set; }
        //[NotMapped]
        //public string DepartmentName { get; set; }
        //[NotMapped]
        //public string SkillName { get; set; }
    }
    

    

}
