
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;



namespace WebApplicationn.Database.Db
{
    public class Connection
    {
        MySql.Data.MySqlClient.MySqlConnection con;

        public Connection()
        {
           
            string constr = "server=localhost;port=3306;user=root;password=ho*V_3#Sw*ak2E;database=dreamy";
            con = new MySqlConnection(constr);
        }

        public void Open()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
            catch (Exception ex)
            {
            }
        }

        public void Close()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

        }

        public MySqlConnection Config()
        {
            return con;
        }
    }
}
