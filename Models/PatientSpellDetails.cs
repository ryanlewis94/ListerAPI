using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechAPITest.Models
{
    public class PatientSpellDetails
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime AdmitDate { get; set; }
        public string Notes { get; set; }
        public string Ward { get; set; }
    }
}
