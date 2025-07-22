using System;
using System.Collections.Generic;

namespace PRN_SafeDrive_Aplication.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public string ReportType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string Status { get; set; } = null!;

    public string ReferenceType { get; set; } = null!;

    public int ReferenceId { get; set; }

    public virtual User User { get; set; } = null!;
}
