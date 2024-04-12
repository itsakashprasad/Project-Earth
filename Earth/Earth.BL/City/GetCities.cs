using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Earth;
using System.Windows;

namespace Earth
{
    public class GetCities
    {

        public List<CityEntities> GetCitiesFromDatabase(out string returnResult, out int returnValue)
        {
            List<CityEntities> cities = new List<CityEntities>();
            returnResult = string.Empty;
            returnValue = -1;

            try
            {
                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();

                MySqlCommand command = new MySqlCommand("GetCities", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
                command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    CityEntities city = new CityEntities
                    {
                        Guid = reader["Guid"].ToString(),
                        StateGuid = reader["StateGuid"].ToString(),
                        StateName = reader["StateName"].ToString(),
                        CountryName = reader["CountryName"].ToString(),
                        Name = reader["Name"].ToString(),
                        CityCode= reader["CityCode"].ToString(),
                        IsValid = Convert.ToBoolean(reader["IsValid"]),
                        IsDeleted = reader.IsDBNull(reader.GetOrdinal("IsDeleted")) ? false : Convert.ToBoolean(reader["IsDeleted"]),
                        AddBy = reader["AddBy"].ToString(),
                        AddDate = Convert.ToDateTime(reader["AddDate"]),
                        UpdateBy = reader.IsDBNull(reader.GetOrdinal("UpdateBy")) ? string.Empty : reader["UpdateBy"].ToString(),
                        UpdateDate = reader.IsDBNull(reader.GetOrdinal("UpdateDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["UpdateDate"])
                    };

                    cities.Add(city);
                }


                returnResult = command.Parameters["@p_Result"].Value?.ToString();
                returnValue = Convert.ToInt32(command.Parameters["@p_Return"].Value);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return cities;
        }

    }
}

