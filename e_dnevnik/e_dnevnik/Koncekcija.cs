using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace e_dnevnik
{
    class Koncekcija
    {
        static public SqlConnection Connect()
        {
            string CS;
            CS = ConfigurationManager.ConnectionStrings["home"].ConnectionString;
            SqlConnection connection = new SqlConnection(CS);
            return connection;
        }
    }
}