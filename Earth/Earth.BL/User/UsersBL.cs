using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Earth.MagicDL;


namespace Earth
{
   public class UsersBL
    {
        public List<UsersEntity> Getusers(string Guid, out int ReturnValue, out string ReturnMessage)
        {
            List<UsersEntity> ListOfppsusers = new List<UsersEntity>();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GD = new MySqlParameter("p_Guid", Guid);
                SqlParamCollection.Add(GD);
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
                MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
                DS = DLComFunctions.ExecuteStoredProcedure("Getusers", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var Tabs = DS.Tables[0];
                    ListOfppsusers = Tabs.AsEnumerable()
                    .Select(row => new UsersEntity
                    {
                        //RowIndex = Tabs.Rows.IndexOf(row) + 1,
                        Guid = row.Field<String>("Guid"),
                        Name = row.Field<String>("Name"),
                        EmailId = row.Field<String>("EmailId"),
                        Password = row.Field<String>("Password"),
                        PhoneNo = row.Field<String>("PhoneNo"),
                        //ImagePath = row.Field<String>("ImagePath"),
                        AllowAccessFrom = row.Field<DateTime>("AllowAccessFrom"),
                        AllowAccessTill = row.Field<DateTime>("AllowAccessTill"),
                        ActivationDate = row.Field<DateTime>("ActivationDate"),
                        //Otp = row.Field<String>("Otp"),
                        IsValid = row.Field<bool>("IsValid"),
                        IsDeleted = row.Field<bool>("IsDeleted"),
                        AddBy = row.Field<String>("AddBy"),
                        AddDate = row.Field<DateTime>("AddDate"),
                        UpdateBy = row.Field<String>("UpdateBy"),
                        UpdateDate = row.Field<DateTime>("UpdateDate"),
                    }).ToList();
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfppsusers;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfppsusers;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                return ListOfppsusers;
            }
        }
        public void Upsertusers(UsersEntity objParam, out string ReturnGuid, out int ReturnValue, out string ReturnMessage)
        {
            try
            {
                objParam.AddBy = SiteRegistration.objRegistration.Guid;
                objParam.UpdateBy = SiteRegistration.objRegistration.Guid;
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GGuid = new MySqlParameter("p_Guid", objParam.Guid);
                SqlParamCollection.Add(GGuid);
                MySqlParameter GName = new MySqlParameter("p_Name", objParam.Name);
                SqlParamCollection.Add(GName);
                MySqlParameter GEmailId = new MySqlParameter("p_EmailId", objParam.EmailId);
                SqlParamCollection.Add(GEmailId);
                MySqlParameter GPassword = new MySqlParameter("p_Password", objParam.Password);
                SqlParamCollection.Add(GPassword);
                MySqlParameter GPhoneNo = new MySqlParameter("p_PhoneNo", objParam.PhoneNo);
                SqlParamCollection.Add(GPhoneNo);
                //MySqlParameter GImagePath = new MySqlParameter("p_ImagePath", "");
                //SqlParamCollection.Add(GImagePath);
                MySqlParameter GAllowAccessFrom = new MySqlParameter("p_AllowAccessFrom", objParam.AllowAccessFrom);
                SqlParamCollection.Add(GAllowAccessFrom);
                MySqlParameter GAllowAccessTill = new MySqlParameter("p_AllowAccessTill", objParam.AllowAccessTill);
                SqlParamCollection.Add(GAllowAccessTill);
                MySqlParameter GActivationDate = new MySqlParameter("p_ActivationDate", objParam.ActivationDate);
                SqlParamCollection.Add(GActivationDate);
                //MySqlParameter GOtp = new MySqlParameter("p_Otp", objParam.Otp);
                //SqlParamCollection.Add(GOtp);
                MySqlParameter GIsValid = new MySqlParameter("p_IsValid", objParam.IsValid);
                SqlParamCollection.Add(GIsValid);
                MySqlParameter GIsDeleted = new MySqlParameter("p_IsDeleted", objParam.IsDeleted);
                SqlParamCollection.Add(GIsDeleted);
                MySqlParameter GAddBy = new MySqlParameter("p_AddBy", objParam.AddBy);
                SqlParamCollection.Add(GAddBy);
                MySqlParameter GAddDate = new MySqlParameter("p_AddDate", DateTime.Now);
                SqlParamCollection.Add(GAddDate);
                MySqlParameter GUpdateBy = new MySqlParameter("p_UpdateBy", objParam.UpdateBy);
                SqlParamCollection.Add(GUpdateBy);
                MySqlParameter GUpdateDate = new MySqlParameter("p_UpdateDate", DateTime.Now);
                SqlParamCollection.Add(GUpdateDate);
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
                MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
                DS = DLComFunctions.ExecuteStoredProcedure("Upsertusers", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    ReturnGuid = (objParam.Guid == new Guid().ToString()) ? DS.Tables[0].Rows[0]["ReturnGuid"].ToString() : objParam.Guid;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    ReturnGuid = new Guid().ToString();
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                ReturnGuid = new Guid().ToString();
            }
        }
        public void UpdateusersFilePath(string Type, UsersEntity objParam, string ReturnGuid, out int ReturnValue, out string ReturnMessage)
        {
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GType = new MySqlParameter("p_Type", Type);
                SqlParamCollection.Add(GType);
                MySqlParameter GReturnGuid = new MySqlParameter("p_Guid", ReturnGuid);
                SqlParamCollection.Add(GReturnGuid);
                //MySqlParameter GImagePath = new MySqlParameter("p_ImagePath", objParam.ImagePath);
                //SqlParamCollection.Add(GImagePath);
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
                MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
                DS = DLComFunctions.ExecuteStoredProcedure("UpdateusersFilePath", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
            }
        }

        public List<UsersEntity> GetOperator(out int ReturnValue, out string ReturnMessage)
        {
            List<UsersEntity> ListOfppsusers = new List<UsersEntity>();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                 
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
                MySQLDLCommonFunctions DLComFunctions = new MySQLDLCommonFunctions();
                DS = DLComFunctions.ExecuteStoredProcedure("GetOperator", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var Tabs = DS.Tables[0];
                    ListOfppsusers = Tabs.AsEnumerable()
                    .Select(row => new UsersEntity
                    {
                        RowIndex = Tabs.Rows.IndexOf(row) + 1,
                        Guid = row.Field<String>("Guid"),
                        Name = row.Field<String>("Name"),
                        EmailId = row.Field<String>("EmailId"),
                        Password = row.Field<String>("Password"),
                        PhoneNo = row.Field<String>("PhoneNo"),
                        //ImagePath = row.Field<String>("ImagePath"),
                        AllowAccessFrom = row.Field<DateTime>("AllowAccessFrom"),
                        AllowAccessTill = row.Field<DateTime>("AllowAccessTill"),
                        ActivationDate = row.Field<DateTime>("ActivationDate"),
                        //Otp = row.Field<String>("Otp"),
                        IsValid = row.Field<Boolean>("IsValid"),
                        IsDeleted = row.Field<Boolean>("IsDeleted"),
                        AddBy = row.Field<String>("AddBy"),
                        AddDate = row.Field<DateTime>("AddDate"),
                        UpdateBy = row.Field<String>("UpdateBy"),
                        UpdateDate = row.Field<DateTime>("UpdateDate"),
                    }).ToList();
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfppsusers;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfppsusers;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                return ListOfppsusers;
            }
        }
    }
}
