using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace KursovayaBelya
{
    class DataBase
    {
        MySqlConnection sqlConnection = new MySqlConnection("server=localhost;port=3306;database=airline_db;user=root;password=qqwrd;");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
