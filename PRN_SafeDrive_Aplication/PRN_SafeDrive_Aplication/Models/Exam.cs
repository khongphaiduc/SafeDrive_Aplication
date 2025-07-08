using System;
using System.Collections.Generic;

namespace PRN_SafeDrive_Aplication.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int CourseId { get; set; }

    public DateOnly Date { get; set; }

    public string Room { get; set; } = null!;

    public int ExamerId { get; set; }
    
   
    public string Status { get; set; }

    public int IDCertificates { get; set; } 

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
