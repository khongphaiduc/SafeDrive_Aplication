using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.Models
{
    internal class CertificateDto
    {
        public string StudentName { get; set; }
        public string CertificateCode { get; set; }
        public string IssuedDate { get; set; }
        public string ExpirationDate { get; set; }
        public string CertificateCodeID { get; set; }
    }
}
