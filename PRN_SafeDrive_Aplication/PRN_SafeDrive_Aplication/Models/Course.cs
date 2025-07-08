using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN_SafeDrive_Aplication.Models;

public partial class Course
{
    public int CourseId { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string CourseName { get; set; } = null!;

    public int TeacherId { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string ContentCourse {  get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual User Teacher { get; set; } = null!;
}
