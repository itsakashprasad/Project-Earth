using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class Country
    {
        public List<CountryEntities> GetCountries(string Guid, out int ReturnValue, out string ReturnMessage)
        {
            List<CountryEntities> ListOfCountry = new List<CountryEntities>();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter guid = new MySqlParameter("p_Guid", Guid);
                SqlParamCollection.Add(guid);
                string RetResult = "";
                MySqlParameter Result = new MySqlParameter("p_Result", typeof(string));
                Result.Value = RetResult;
                Result.Size = 4000;
                Result.Direction = System.Data.ParameterDirection.Output;
                SqlParamCollection.Add(Result);
                MySqlParameter RetInt = new MySqlParameter("p_Return", typeof(Int32));
                RetInt.Value = ErrCode;
                RetInt.Direction = System.Data.ParameterDirection.Output;
                SqlParamCollection.Add(RetInt);
                //MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
                //DS = DLComFunctions.ExecuteStoredProcedure("GetCountries", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var Tabs = DS.Tables[0];
                    ListOfCountry = Tabs.AsEnumerable()
                    .Select(row => new CountryEntities
                    {
                       
                        Guid = row.Field<String>("Guid"),
                        Name = row.Field<String>("Name"),
                        CountryCode = row.Field<String>("CountryCode"),
                        IsValid = row.Field<Boolean>("IsValid"),
                        IsDeleted = row.Field<Boolean>("IsDeleted"),
                        AddBy = row.Field<String>("AddBy"),
                        AddDate = row.Field<DateTime>("AddDate"),
                        UpdateBy = row.Field<String>("UpdateBy"),
                        UpdateDate = row.Field<DateTime>("UpdateDate"),
                    }).ToList();
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfCountry;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfCountry;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                return ListOfCountry;
            }
        }
        //public void UpsertCountry(CountryEntities objParam, out int ReturnValue, out string ReturnMessage)
        //{
        //    try
        //    {
        //        objParam.AddBy = Classes.SiteRegistration.objRegistration.Guid;
        //        objParam.UpdateBy = Classes.SiteRegistration.objRegistration.Guid;
        //        int ErrCode = -1;
        //        DataSet DS = new DataSet();
        //        var SqlParamCollection = new List<MySqlParameter>();
        //        MySqlParameter GGuid = new MySqlParameter("p_Guid", objParam.Guid);
        //        SqlParamCollection.Add(GGuid);
        //        MySqlParameter GName = new MySqlParameter("p_Name", objParam.Name);
        //        SqlParamCollection.Add(GName);
        //        MySqlParameter p_CountryCode = new MySqlParameter("p_CountryCode", objParam.CountryCode);
        //        SqlParamCollection.Add(p_CountryCode);
        //        MySqlParameter GIsValid = new MySqlParameter("p_IsValid", objParam.IsValid);
        //        SqlParamCollection.Add(GIsValid);
        //        MySqlParameter GIsDeleted = new MySqlParameter("p_IsDeleted", objParam.IsDeleted);
        //        SqlParamCollection.Add(GIsDeleted);
        //        MySqlParameter GAddBy = new MySqlParameter("p_AddBy", objParam.AddBy);
        //        SqlParamCollection.Add(GAddBy);
        //        MySqlParameter GAddDate = new MySqlParameter("p_AddDate", DateTime.Now);
        //        SqlParamCollection.Add(GAddDate);
        //        MySqlParameter GUpdateBy = new MySqlParameter("p_UpdateBy", objParam.UpdateBy);
        //        SqlParamCollection.Add(GUpdateBy);
        //        MySqlParameter GUpdateDate = new MySqlParameter("p_UpdateDate", DateTime.Now);
        //        SqlParamCollection.Add(GUpdateDate);
        //        string RetResult = "";
        //        MySqlParameter Result = new MySqlParameter("p_Result", typeof(string));
        //        Result.Value = RetResult;
        //        Result.Size = 4000;
        //        Result.Direction = System.Data.ParameterDirection.Output;
        //        SqlParamCollection.Add(Result);
        //        MySqlParameter RetInt = new MySqlParameter("p_Return", typeof(Int32));
        //        RetInt.Value = ErrCode;
        //        RetInt.Direction = System.Data.ParameterDirection.Output;
        //        SqlParamCollection.Add(RetInt);
        //        MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
        //        DS = DLComFunctions.ExecuteStoredProcedure("UpsertCountry", SqlParamCollection, ref RetResult, ref ErrCode);
        //        if (ErrCode == 0)
        //        {
        //            ReturnMessage = RetResult;
        //            ReturnValue = ErrCode;
        //        }
        //        else
        //        {
        //            ReturnMessage = RetResult;
        //            ReturnValue = ErrCode;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ReturnMessage = ex.Message;
        //        ReturnValue = -1;
        //    }
        //}
    }
}
