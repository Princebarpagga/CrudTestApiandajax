using System;
using System.Collections.Generic;

namespace CrudTestApi.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public decimal Salary { get; set; }

    public int? DepartmentId { get; set; }

    public int? SkillId { get; set; }

    public string? Email { get; set; }
}
