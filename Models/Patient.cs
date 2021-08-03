using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechAPITest.Models
{
    public class Patient
    {
        public Guid PatientID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
