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
    public class GetPages
    {
        public List<PagesEntities> GetPagesFromDatabase(out string returnResult, out int returnValue)
        {
            List<PagesEntities> PagesList = new List<PagesEntities>();

            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("GetPages", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
            command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PagesEntities Pages = new PagesEntities
                {
                    Guid = reader["Guid"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    IsValid = Convert.ToBoolean(reader["IsValid"])

                };

                PagesList.Add(Pages);
            }



            returnResult = command.Parameters["@p_Result"]?.Value?.ToString();
            returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);

            return PagesList;
        }
    }
}
