using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        public static SqlConnection GetConnection()
        {                               
            String connectionString = @"Data Source = localhost; Initial Catalog = mtg_db; Integrated Security = True";

            var connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
