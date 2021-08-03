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
    public class SpellRepo : ISpellRepo
    {
        private readonly string _connStr;
        public SpellRepo(IOptions<ConnectionStrings> connectionStrings)
        {
            _connStr = connectionStrings.Value.DefaultConnectionString;
        }

        /// <summary>
        /// returns spell details with the given id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Spell GetActiveSpellByPatientId(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.QueryFirstOrDefault<Spell>("SELECT * FROM Spell WHERE fkPatientID = @FKPATIENTID AND Active = '1'",
                    new { FKPATIENTID = Id });
            }
        }

        /// <summary>
        /// executes a stored proc that admits a patient
        /// </summary>
        /// <param name="spell"></param>
        public void AdmitPatient(Spell spell)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Execute(@"EXECUTE AdmitPatient @ADMITDATE, @NOTES, @ACTIVE, @WARDID, @PATIENTID",
                    new
                    {
                        ADMITDATE = spell.AdmitDate,
                        NOTES = spell.Notes,
                        ACTIVE = "1",
                        WARDID = spell.wardID,
                        PATIENTID = spell.patientID
                    });
            }
        }

        /// <summary>
        /// executes a stored proc that discharges a patient
        /// </summary>
        /// <param name="spell"></param>
        public void DischargePatient(Spell spell)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                connection.Execute(@"EXECUTE DischargePatient @SPELLID, @DISCHARGEDATE, @NOTES, @ACTIVE, @WARDID",
                    new
                    {
                        SPELLID = spell.SpellID,
                        DISCHARGEDATE = spell.DischargeDate,
                        NOTES = spell.Notes,
                        ACTIVE = "0",
                        WARDID = spell.wardID,
                    });
            }
        }
    }
}
