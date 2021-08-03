using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAPITest.Models;

namespace TechAPITest.Data.Interfaces
{
    public interface ISpellRepo
    {
        Spell GetActiveSpellByPatientId(Guid Id);
        void AdmitPatient(Spell spell);
        void DischargePatient(Spell spell);
    }
}
