using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Online_Grocery_Store
{
    public class DatabaseHelper
    {
        private static string connectionString = "User Id=system;Password=student;Data Source=localhost:1521/xe";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}
