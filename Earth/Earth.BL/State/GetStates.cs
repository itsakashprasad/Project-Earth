using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Earth;
using System.Windows;

namespace Earth
{
    public class GetStates
    {

        public List<StateEntities> GetStatesFromDatabase(string Mode, out string returnResult, out int returnValue)
        {
            List<StateEntities> States = new List<StateEntities>();

            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("GetStates", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
            command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                StateEntities State = new StateEntities
                {
                    Guid = reader["Guid"].ToString(),
                    CountryGuid = reader["CountryGuid"].ToString(),
                    CountryName = reader["CountryName"].ToString(),
                    Name = reader["Name"].ToString(),
                    StateCode = reader["StateCode"].ToString(),
                    IsValid = Convert.ToBoolean(reader["IsValid"]),
                    IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false,
                    AddBy = reader["AddBy"].ToString(),
                    AddDate = Convert.ToDateTime(reader["AddDate"]),
                    UpdateBy = reader["UpdateBy"] != DBNull.Value ? reader["UpdateBy"].ToString() : string.Empty,
                    UpdateDate = reader["UpdateDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdateDate"]) : DateTime.MinValue
                };

                States.Add(State);
            }



            returnResult = command.Parameters["@p_Result"]?.Value?.ToString();
            returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);

            return States;
        }

    }
}
