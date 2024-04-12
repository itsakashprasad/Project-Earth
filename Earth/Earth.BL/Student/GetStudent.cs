using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Earth
{
    public class GetStudent
    {
        public List<StudentEntities> GetStudentFromDatabase(out string returnResult, out int returnValue)
        {
            List<StudentEntities> StudentEntityList = new List<StudentEntities>();

            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("GetStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
             
            
            command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
            command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;


            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                StudentEntities StudentEntity = new StudentEntities
                {
                    Guid = reader["Guid"].ToString(),
                    Name = reader["Name"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    IsValid = Convert.ToBoolean(reader["IsValid"]),
                    IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false,
                    AddBy = reader["AddBy"].ToString(),
                    AddDate = Convert.ToDateTime(reader["AddDate"]),
                    UpdateBy = reader["UpdateBy"] != DBNull.Value ? reader["UpdateBy"].ToString() : string.Empty,
                    UpdateDate = reader["UpdateDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdateDate"]) : DateTime.MinValue
                };

                StudentEntityList.Add(StudentEntity);
            }



            returnResult = command.Parameters["@p_Result"]?.Value?.ToString();
            returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);

            return StudentEntityList;
        }
    }
}
