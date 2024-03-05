using System;
using System.Collections.Generic;

namespace CrudTestApi.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string SkillName { get; set; } = null!;
}
