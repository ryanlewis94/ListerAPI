using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechAPITest.Data.Interfaces;
using TechAPITest.Models;

namespace TechAPITest.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Patients.Spell)]
    public class PatientSpellController : ControllerBase
    {
        private readonly IPatientRepo _patientRepo;
        private readonly ISpellRepo _spellRepo;
        private readonly IWardRepo _wardRepo;
        

        public PatientSpellController(IPatientRepo patientRepo, ISpellRepo spellRepo, IWardRepo wardRepo)
        {
            _patientRepo = patientRepo;
            _spellRepo = spellRepo;
            _wardRepo = wardRepo;
        }

        /// <summary>
        /// admit a patient
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{admit}")]
        public ActionResult AdmitPatient(Spell spell)
        {
            var patient = _patientRepo.GetPatientById(spell.patientID);
            var ward = _wardRepo.GetWardById(spell.wardID);

            //checks to make sure there is room in the ward
            if (ward.SpacesLeft < 1)
                return StatusCode(409, $"Ward {ward.Name} currently has no room left, try another ward.");
            //check to make sure there currently isn't any active spells
            if (_spellRepo.GetActiveSpellByPatientId(spell.patientID) != null)
                return StatusCode(409, $"Patient {patient.Name} is already admitted and can't be admitted again until they have been discharged");

            _spellRepo.AdmitPatient(spell);

            return Ok("Patient successfully admitted");
        }

        /// <summary>
        /// discharge a patient
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{discharge}")]
        public ActionResult DischargePatient(Spell spell)
        {
            var patient = _patientRepo.GetPatientById(spell.patientID);

            //checks to make sure there is a spell that can be discharged
            if (_spellRepo.GetActiveSpellByPatientId(spell.patientID) == null)
                return StatusCode(409, $"This spell does not exist, or {patient.Name} isn't currently admitted. Please admit a patient before discharging");

            _spellRepo.DischargePatient(spell);

            return Ok("Patient successfully discharged");
        }
    }
}
