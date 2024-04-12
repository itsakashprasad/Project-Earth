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
    public class GetGroups
    {
        public List<GroupsEntities> GetGroupsFromDatabase(out string returnResult, out int returnValue)
        {
            List<GroupsEntities> GroupsList = new List<GroupsEntities>();

            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("GetGroups", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
            command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GroupsEntities Groups = new GroupsEntities
                {
                    Guid = reader["Guid"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    IsValid = Convert.ToBoolean(reader["IsValid"])

                };

                GroupsList.Add(Groups);
            }



            returnResult = command.Parameters["@p_Result"]?.Value?.ToString();
            returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);

            return GroupsList;
        }
    }
}
