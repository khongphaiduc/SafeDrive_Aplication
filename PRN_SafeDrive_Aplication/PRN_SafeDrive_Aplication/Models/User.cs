using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN_SafeDrive_Aplication.Models;

public partial class User
{
    public int UserId { get; set; }

    [Column(TypeName ="nvarchar(200)")]
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Class { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string? School { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
