using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertCountry
    {
        public void ExecuteUpsertCountry(CountryEntities country)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@p_Mode", country.Mode);
                        command.Parameters.AddWithValue("@p_Guid", country.Guid);
                        command.Parameters.AddWithValue("@p_Name", country.Name);
                        command.Parameters.AddWithValue("@p_CountryCode", country.CountryCode);
                        command.Parameters.AddWithValue("@p_IsValid", country.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", country.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", country.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", country.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", country.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", country.UpdateDate);

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
