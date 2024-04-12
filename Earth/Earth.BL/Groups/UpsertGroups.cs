using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertGroups
    {
        public void ExecuteUpsertGroups(GroupsEntities Groups)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertGroups", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@p_Mode", Groups.Mode);
                        command.Parameters.AddWithValue("@p_Guid", Groups.Guid);
                        command.Parameters.AddWithValue("@p_Name", Groups.Name);
                        command.Parameters.AddWithValue("@p_Description", Groups.Description);
                        command.Parameters.AddWithValue("@p_IsValid", Groups.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", Groups.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", Groups.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", Groups.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", Groups.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", Groups.UpdateDate);

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
