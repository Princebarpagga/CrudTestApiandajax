namespace Angulartestapi.Controllers
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public int? SkillId { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string SkillName { get; set; }
    }
}