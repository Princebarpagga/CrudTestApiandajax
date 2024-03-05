using System;
using System.Collections.Generic;

namespace CrudTestApi.Models;

public partial class TblQualification
{
    public int QualifId { get; set; }

    public string QualificationName { get; set; } = null!;
}
