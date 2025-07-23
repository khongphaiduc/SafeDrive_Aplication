using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.Models
{
    internal class StudentScoreDTO
    {
        public string? StudentName { get; set; }
        public string? CourseName { get; set; }
        public DateTime ExamDate { get; set; }
        public double Score { get; set; }
        public string? PassStatus { get; set; }
    }
}
