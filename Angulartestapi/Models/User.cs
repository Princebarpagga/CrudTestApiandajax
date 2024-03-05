using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angulartestapi.Models;

public partial class User
{
    [Required]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "MobileNumber is required")]
    public string MobileNumber { get; set; } = null!;

    public bool ShowUserPassword { get; set; }
    [Required(ErrorMessage = "ProfilePicture is required")]
    public string? ProfilePicture { get; set; }
}
