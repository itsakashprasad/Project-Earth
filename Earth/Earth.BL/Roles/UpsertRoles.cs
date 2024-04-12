using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertRoles
    {
        public void ExecuteUpsertRoles(RolesEntities Roles)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertRoles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@p_Mode", Roles.Mode);
                        command.Parameters.AddWithValue("@p_Guid", Roles.Guid);
                        command.Parameters.AddWithValue("@p_Name", Roles.Name);
                        command.Parameters.AddWithValue("@p_Description", Roles.Description);
                        command.Parameters.AddWithValue("@p_RoleType", Roles.RoleType);
                        command.Parameters.AddWithValue("@p_IsValid", Roles.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", Roles.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", Roles.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", Roles.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", Roles.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", Roles.UpdateDate);

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
