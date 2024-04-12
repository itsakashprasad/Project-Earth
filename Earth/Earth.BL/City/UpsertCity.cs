using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertCity
    {
        public void ExecuteUpsertCity(CityEntities City)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertCity", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_Mode", City.Mode);
                        command.Parameters.AddWithValue("@p_Guid", City.Guid);
                        command.Parameters.AddWithValue("@p_StateGuid", City.StateGuid);
                        //command.Parameters.AddWithValue("@p_CountryName", City.CountryName);
                        command.Parameters.AddWithValue("@p_Name", City.Name);
                        command.Parameters.AddWithValue("@p_CityCode", City.CityCode);
                        command.Parameters.AddWithValue("@p_IsValid", City.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", City.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", City.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", City.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", City.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", City.UpdateDate);

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

