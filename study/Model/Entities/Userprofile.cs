using System;
using System.Collections.Generic;

namespace study.Model.Entities;

public partial class Userprofile
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;
}
