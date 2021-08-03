using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAPITest.Data.Interfaces;
using TechAPITest.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;

namespace TechAPITest.Data.Repositories
{
    public class WardRepo : IWardRepo
    {
        private readonly string _connStr;
        public WardRepo(IOptions<ConnectionStrings> connectionStrings)
        {
            _connStr = connectionStrings.Value.DefaultConnection;
        }

        /// <summary>
        /// returns ward details with the given id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Ward GetWardById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                return connection.QueryFirstOrDefault<Ward>(@"SELECT * 
                                           FROM Ward 
                                           WHERE WardID = @ID", new { ID = Id });
            }
        }
    }
}
