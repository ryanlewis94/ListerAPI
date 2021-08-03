using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechAPITest.Models
{
    public class Spell
    {
        public Guid SpellID { get; set; }
        public DateTime AdmitDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }
        public int wardID { get; set; }
        public Guid patientID { get; set; }
    }
}
