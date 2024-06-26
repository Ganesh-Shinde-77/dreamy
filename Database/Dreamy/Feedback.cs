
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using WebApplicationn.Models;
using WebApplicationn.Database.Db;


namespace WebApplicationn.Database.Dreamy
{
    public class Feedback
    {
        public static string AddFeedback(Records record)
        {

            Connection connection = new Connection();

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();

                string query = "Insert into dreamy.feedback (name,emailAddress,message) values ('" + record.name + "','" + record.emailAddress + "','" + record.message + "')";

                cmd = new MySqlCommand(query, connection.Config());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

            return "Success";
        }


    }
}
