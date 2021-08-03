using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using TechAPITest.Data.Interfaces;
using TechAPITest.Models;

namespace TechAPITest.Data.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly string _connStr;
        public PatientRepo(IOptions<ConnectionStrings> connectionStrings)
        {
            _connStr = connectionStrings.Value.DefaultConnectionString;
        }

        /// <summary>
        /// returns a patient with the given id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Patient GetPatientById(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.QueryFirstOrDefault<Patient>(@"SELECT * 
                                           FROM Patient 
                                           WHERE PatientID = @ID", new { ID = Id });
            }
        }

        /// <summary>
        /// returns a patient to check for duplicate patients
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public Patient FindPatient(string name, DateTime dateOfBirth, string gender)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.QueryFirstOrDefault<Patient>(@"SELECT * FROM Patient
                                                                    WHERE Name = @NAME
                                                                    AND DateOfBirth = @DATEOFBIRTH
                                                                    AND Gender = @GENDER",
                    new {NAME = name, DATEOFBIRTH = dateOfBirth, GENDER = gender});
            }
        }
        
        /// <summary>
        /// executes stored proc that adds new patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Patient AddNewPatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Execute(@"EXECUTE InsertPatient @NAME, @DATEOFBIRTH, @GENDER",
                    new
                    {
                        NAME = patient.Name,
                        DATEOFBIRTH = patient.DateOfBirth,
                        GENDER = patient.Gender
                    });

                return FindPatient(patient.Name, patient.DateOfBirth, patient.Gender);
            }
        }

        /// <summary>
        /// executes a stored proc that updates patients details
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Patient UpdatePatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Execute(@"EXECUTE UpdatePatient @PATIENTID, @NAME, @DATEOFBIRTH, @GENDER",
                    new
                    {
                        PATIENTID = patient.PatientID,
                        NAME = patient.Name,
                        DATEOFBIRTH = patient.DateOfBirth,
                        GENDER = patient.Gender
                    });

                return GetPatientById(patient.PatientID);
            }
        }

        /// <summary>
        /// returns a list of patients that are active
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientSpellDetails> GetActivePatients()
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.Query<PatientSpellDetails>(@"SELECT Patient.Name, Patient.DateOfBirth, Patient.Gender, Spell.AdmitDate, Spell.Notes, Ward.Name AS Ward
                                                                 FROM ((Spell
                                                                 INNER JOIN Patient ON Spell.fkPatientID = Patient.PatientId)
                                                                 INNER JOIN Ward ON Spell.fkWardID = Ward.WardId)
                                                                 WHERE Spell.Active = 1");
            }
        }

        /// <summary>
        /// returns a list of patients that are active in a certain ward
        /// </summary>
        /// <param name="wardId"></param>
        /// <returns></returns>
        public IEnumerable<PatientSpellDetails> GetActivePatientsByWard(int wardId)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.Query<PatientSpellDetails>(@"SELECT Patient.Name, Patient.DateOfBirth, Patient.Gender, Spell.AdmitDate, Spell.Notes, Ward.Name AS Ward
                                                                 FROM ((Spell
                                                                 INNER JOIN Patient ON Spell.fkPatientID = Patient.PatientId)
                                                                 INNER JOIN Ward ON Spell.fkWardID = Ward.WardId)
                                                                 WHERE Spell.Active = 1
                                                                 AND Ward.WardId = @WARDID", new {WARDID = wardId});
            }
        }
    }
}
