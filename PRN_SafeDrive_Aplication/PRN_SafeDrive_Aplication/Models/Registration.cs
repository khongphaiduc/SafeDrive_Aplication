using System;
using System.Collections.Generic;

namespace PRN_SafeDrive_Aplication.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string Status { get; set; } = null!;

    public string? Comments { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
