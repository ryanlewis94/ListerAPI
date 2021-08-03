using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechAPITest.Data.Interfaces;
using TechAPITest.Data.Repositories;
using TechAPITest.Models;

namespace TechAPITest.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Patients.Route)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo _patientRepo;

        public PatientController(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        /// <summary>
        /// add a patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{insert}")]
        public ActionResult<Patient> InsertPatient(Patient patient)
        {
            //check for duplicate patient
            if (_patientRepo.FindPatient(patient.Name, patient.DateOfBirth, patient.Gender) != null)
                return StatusCode(409, $"Patient {patient.Name} with DOB of {patient.DateOfBirth} and a gender of {patient.Gender} already exists");

            var patientAdded = _patientRepo.AddNewPatient(patient);

            return Ok(patientAdded);
        }

        /// <summary>
        /// update a patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{update}")]
        public ActionResult<Patient> UpdatePatient(Patient patient)
        {
            //check to make sure that the patient trying to be updated exists
            if (_patientRepo.GetPatientById(patient.PatientID) == null) 
                return NotFound("That Patient does not exist");
            //check for duplicate patients
            if (_patientRepo.FindPatient(patient.Name, patient.DateOfBirth, patient.Gender) != null)
                return StatusCode(409, $"Patient {patient.Name} with DOB of {patient.DateOfBirth} and a gender of {patient.Gender} already exists");

            var patientUpdated = _patientRepo.UpdatePatient(patient);

            return Ok(patientUpdated);
        }
    }
}
