using System;
using System.Collections.Generic;

namespace CrudTestApi.Models;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int DepId { get; set; }

    public int QualifId { get; set; }

    public decimal Percentage { get; set; }
}
