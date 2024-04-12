using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Earth
{
    public class GetUser
    {
        public List<UserEntities> GetUserFromDatabase(out string returnResult, out int returnValue)
        {
            List<UserEntities> UserList = new List<UserEntities>();

            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("GetUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
            command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                UserEntities User = new UserEntities
                {
                    Guid = reader["Guid"].ToString(),
                    Name = reader["Name"].ToString(),
                    Password = reader["Password"].ToString(),
                    EmailId = reader["EmailId"].ToString(),
                    PhoneNo = reader["PhoneNo"].ToString(),
                    IsValid = Convert.ToBoolean(reader["IsValid"])
                   
                };

                UserList.Add(User);
            }



            returnResult = command.Parameters["@p_Result"]?.Value?.ToString();
            returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);

            return UserList;
        }
    }
}
