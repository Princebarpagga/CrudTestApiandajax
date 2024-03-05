using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codeFirstCrud.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int SkillId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        //    [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]~-}+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9]*[a-z0-9])?\.)
        //+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please ener valid email adress")]
        public string Email { get; set; }
        public Department Department { get; set; }
        public Skill Skill { get; set; }
    }
}
