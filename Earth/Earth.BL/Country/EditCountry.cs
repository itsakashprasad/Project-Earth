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
    public class EditCountry
    {
        public void ExecuteEditCountry(CountryEntities Country)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("EditCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@p_Guid", Country.Guid);
                        command.Parameters.AddWithValue("@p_Name", Country.Name);
                        command.Parameters.AddWithValue("@p_CountryCode", Country.CountryCode);
                        command.Parameters.AddWithValue("@p_IsValid", Country.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_UpdateBy", Country.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", Country.UpdateDate);

                        command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        string result = command.Parameters["@p_Result"].Value.ToString();
                        int returnCode = Convert.ToInt32(command.Parameters["@p_Return"].Value);

                        MessageBox.Show(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
