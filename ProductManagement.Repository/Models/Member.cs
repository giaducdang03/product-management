using System;
using System.Collections.Generic;

namespace ProductManagement.Repository.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Role {  get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
