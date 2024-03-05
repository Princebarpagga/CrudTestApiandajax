using System;
using System.Collections.Generic;

namespace CrudTestApi.Models;

public partial class TblDepartment
{
    public int DepId { get; set; }

    public string DepartmentName { get; set; } = null!;
}
