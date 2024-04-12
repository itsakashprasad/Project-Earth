using System;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Earth
{
    public class UpsertUser
    {
        public void ExecuteUpsertUser(UserEntities User)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpsertUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@p_Mode", User.Mode);
                        command.Parameters.AddWithValue("@p_Guid", User.Guid);
                        command.Parameters.AddWithValue("@p_Name", User.Name);
                        command.Parameters.AddWithValue("@p_EmailId", User.EmailId);
                        command.Parameters.AddWithValue("@p_Password", User.Password);
                        command.Parameters.AddWithValue("@p_PhoneNo", User.PhoneNo);
                        command.Parameters.AddWithValue("@p_ActivationDate", User.ActivationDate);
                        command.Parameters.AddWithValue("@p_AllowAccessFrom", User.AllowAccessFrom);
                        command.Parameters.AddWithValue("@p_AllowAccessTill", User.AllowAccessTill);
                        command.Parameters.AddWithValue("@p_IsValid", User.IsValid ? 1 : 0);
                        command.Parameters.AddWithValue("@p_IsDeleted", User.IsDeleted ? 1 : 0);
                        command.Parameters.AddWithValue("@p_AddBy", User.AddBy);
                        command.Parameters.AddWithValue("@p_AddDate", User.AddDate);
                        command.Parameters.AddWithValue("@p_UpdateBy", User.UpdateBy);
                        command.Parameters.AddWithValue("@p_UpdateDate", User.UpdateDate);

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
