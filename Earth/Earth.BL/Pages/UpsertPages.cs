using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertPages
    {
        public void ExecuteUpsertPages(PagesEntities Pages)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertPages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@p_Mode", Pages.Mode);
                        command.Parameters.AddWithValue("@p_Guid", Pages.Guid);
                        command.Parameters.AddWithValue("@p_Name", Pages.Name);
                        command.Parameters.AddWithValue("@p_Description", Pages.Description);
                        command.Parameters.AddWithValue("@p_IsValid", Pages.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", Pages.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", Pages.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", Pages.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", Pages.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", Pages.UpdateDate);

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
