using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAPITest.Models;

namespace TechAPITest.Data.Interfaces
{
    public interface IPatientRepo
    {
        Patient GetPatientById(Guid Id);
        Patient FindPatient(string name, DateTime dateOfBirth, string gender);
        Patient AddNewPatient(Patient patient);
        Patient UpdatePatient(Patient patient);
        IEnumerable<PatientSpellDetails> GetActivePatients();
        IEnumerable<PatientSpellDetails> GetActivePatientsByWard(int wardId);
    }
}
