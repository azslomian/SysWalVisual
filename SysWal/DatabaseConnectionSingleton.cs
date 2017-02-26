using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWal
{
    class DatabaseConnectionSingleton
    {
      /*  private readonly static DatabaseConnectionSingleton _instance = new DatabaseConnectionSingleton();
        private SqlConnectionStringBuilder builder;
        private MySqlConnection connection;
        private DatabaseConnectionSingleton()
        {
            builder = new MySqlConnectionStringBuilder();
            builder.Server = "db1.netakor.com.pl";
            builder.UserID = "c15_dbu_projekt";
            builder.Password = "clinic";
            builder.Database = "c15_dbn_projekt";

            connection = new MySqlConnection(builder.ToString());

        }
        public static MySqlConnection getConnection()
        {
            _instance.connection.Close();
            _instance.connection.Open();
            return _instance.connection;
        }
        public static void CloseConnection()
        {
            _instance.connection.Close();
        }*/
    }
}
