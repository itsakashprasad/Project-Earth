using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertState
    {
        public void ExecuteUpsertState(StateEntities State)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertState", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_Mode", State.Mode);
                        command.Parameters.AddWithValue("@p_Guid", State.Guid);
                        command.Parameters.AddWithValue("@p_CountryGuid", State.CountryGuid);
                        //command.Parameters.AddWithValue("@p_CountryName", State.CountryName);
                        command.Parameters.AddWithValue("@p_Name", State.Name);
                        command.Parameters.AddWithValue("@p_StateCode", State.StateCode);
                        command.Parameters.AddWithValue("@p_IsValid", State.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", State.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", State.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", State.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", State.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", State.UpdateDate);

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
