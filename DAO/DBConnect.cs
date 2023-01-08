using System;
using System.Collections.Generic;
using System.Windows;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DBConnect
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            string strConString = CnnVal("QLHSDb");
            var conn = new SqlConnection(strConString);
            try
            {
                conn.Open();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message,"Timed Out",
                    MessageBoxButton.OK,MessageBoxImage.Error);
                return new SqlConnection();
            }

            return new SqlConnection(strConString);
        }
    }
}
