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
    [Route(ApiRoutes.Patients.Active)]
    public class PatientActiveController : ControllerBase
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IWardRepo _wardRepo;

        public PatientActiveController(IPatientRepo patientRepo, IWardRepo wardRepo)
        {
            _patientRepo = patientRepo;
            _wardRepo = wardRepo;
        }

        /// <summary>
        /// list of active patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<PatientSpellDetails>> GetActivePatients()
        {
            var patients = _patientRepo.GetActivePatients();
            //generate message if no active patients
            if (!patients.Any()) return StatusCode(409, "There is currently no active Patients");

            return Ok(patients);
        }

        /// <summary>
        /// list of active patients in a given ward
        /// </summary>
        /// <param name="wardId"></param>
        /// <returns></returns>
        [HttpGet("ward={wardId}")]
        public ActionResult<IEnumerable<PatientSpellDetails>> GetActivePatientsByWard(int wardId)
        {
            var ward = _wardRepo.GetWardById(wardId);
            //check if ward exists
            if (ward == null) return StatusCode(404, $"Ward with id of {wardId} does not exist");

            var patients = _patientRepo.GetActivePatientsByWard(wardId);
            //generate message if no active patients in the ward
            if (!patients.Any()) return StatusCode(409, $"There is currently no active Patients at Ward {ward.Name}");

            return Ok(patients);
        }
    }
}
