using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.MyModels
{
    public class MExams
    {
        public int IDExam { get; set; }
        public int IDCourse { get; set; }
        public string NameCourse { get; set; } = null!;

        public string CodeCertificate { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Room { get; set; } = null!;

        public string Examer { get; set; } = null!;

        public string Status { get; set; } = null!;

    }
}
