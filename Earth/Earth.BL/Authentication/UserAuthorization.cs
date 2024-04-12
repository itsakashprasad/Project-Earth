using MySql.Data;
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
    public class UserAuthorization
    {
        string AddBy = SiteRegistration.objRegistration.Name;
        string LoginUserGUID = SiteRegistration.objRegistration.Guid;
        public UserEntities UserLogin(string EmailId, string Password, out int SqlResults, out string ReturnMessage)
        {

            UserEntities EntUserRegistrationNew = new UserEntities();
            try
            {
                
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GEmailId = new MySqlParameter("p_EmailId", EmailId);
                SqlParamCollection.Add(GEmailId);
                MySqlParameter GPassword = new MySqlParameter("p_Password", Password);
                SqlParamCollection.Add(GPassword);
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
                DS = DLComFunctions.ExecuteStoredProcedure("UserLogin", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                { 
                    var UserLoginDetail = DS.Tables[0];
                    var UserRoleDetail = DS.Tables[1];
                  
                    EntUserRegistrationNew = UserLoginDetail.AsEnumerable().Select(row => new UserEntities
                    {
                        Guid = row.Field<string>("Guid"),
                        //GroupName = row.Field<string>("GroupName"),
                        Name = String.IsNullOrEmpty(row["Name"].ToString()) ? "" : row.Field<string>("Name"),
                        EmailId = String.IsNullOrEmpty(row["EmailId"].ToString()) ? "" : row.Field<string>("EmailId"),
                        Password = String.IsNullOrEmpty(row["Password"].ToString()) ? "" : row.Field<string>("Password"),
                        PhoneNo = String.IsNullOrEmpty(row["PhoneNo"].ToString()) ? "" : row.Field<string>("PhoneNo"),
                        

                        IsValid = row.Field<Boolean>("IsValid"),
                        IsDeleted = String.IsNullOrEmpty(row["IsDeleted"].ToString()) ? false : row.Field<Boolean>("IsDeleted"),
                        ////AllowAccessFrom = Convert.ToDateTime(row.Field<DateTime?>("AllowAccessFrom")),
                        ////AllowAccessTill = Convert.ToDateTime(row.Field<DateTime?>("AllowAccessTill")),
                        ////ActivationDate = Convert.ToDateTime(row.Field<DateTime?>("ActivationDate")),
                        UpdateBy = String.IsNullOrEmpty(row["UpdateBy"].ToString()) ? "" : row.Field<string>("UpdateBy"),
                        UpdateDate = Convert.ToDateTime(row.Field<DateTime?>("UpdateDate")),
                    }).FirstOrDefault();
                    EntUserRegistrationNew.Roles = UserRoleDetail.AsEnumerable().Select(row => new RolesEntities
                    {
                        Guid = row.Field<string>("Guid"),
                        Name = String.IsNullOrEmpty(row["Name"].ToString()) ? "" : row.Field<string>("Name"),
                        Description = String.IsNullOrEmpty(row["Description"].ToString()) ? "" : row.Field<string>("Description"),
                        RoleType = String.IsNullOrEmpty(row["RoleType"].ToString()) ? "" : row.Field<string>("RoleType"),
                        IsValid = row.Field<Boolean>("IsValid"),
                        IsDeleted = String.IsNullOrEmpty(row["IsDeleted"].ToString()) ? false : row.Field<Boolean>("IsDeleted"),
                        AddBy = String.IsNullOrEmpty(row["AddBy"].ToString()) ? "" : row.Field<string>("AddBy"),
                        AddDate = Convert.ToDateTime(row.Field<DateTime?>("AddDate"))
                    }).ToList();
                    SqlResults = ErrCode;
                    ReturnMessage = RetResult;
                    return EntUserRegistrationNew;
                }
                else
                {
                    SqlResults = ErrCode;
                    ReturnMessage = RetResult;
                    return EntUserRegistrationNew;
                }
            }
            catch (Exception ex)
            {
                SqlResults = -1;
                ReturnMessage = ex.Message;
                return EntUserRegistrationNew;
            }
        }

        public UserEntities GetUserProfileByGuid(out int SqlResults, out string ReturnMessage)
        {
            List<UserEntities> lstEntitRegistration = new List<UserEntities>();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GEmailId = new MySqlParameter("p_Guid", LoginUserGUID);
                SqlParamCollection.Add(GEmailId);

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
                DS = DLComFunctions.ExecuteStoredProcedure("GetUserProfileByGuid", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var Registration = DS.Tables[0];
                    lstEntitRegistration = Registration.AsEnumerable()
                   .Select(row => new UserEntities
                   {
                       Guid = row.Field<string>("Guid"),
                       Name = row.Field<string>("Name"),
                       EmailId = row.Field<string>("EmailId"),
                       Password = row.Field<string>("Password"),
                       PhoneNo = row.Field<string>("PhoneNo"),
                       //ImagePath = row.Field<string>("ImagePath"),
                       //AllowAccessFrom = row.Field<DateTime>("AllowAccessFrom"),
                       //AllowAccessTill = row.Field<DateTime>("AllowAccessTill"),
                       IsValid = Convert.ToBoolean(row.Field<bool?>("IsValid")),
                       IsDeleted = Convert.ToBoolean(row.Field<bool?>("IsDeleted")),
                   }).ToList();
                    SqlResults = ErrCode;
                    ReturnMessage = RetResult;
                    return lstEntitRegistration.FirstOrDefault();
                }
                else
                {
                    SqlResults = ErrCode;
                    ReturnMessage = RetResult;
                    return lstEntitRegistration.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SqlResults = -1;
                ReturnMessage = ex.Message;
                return lstEntitRegistration.FirstOrDefault();
            }
        }

        public void UpsertUserProfile(UserEntities UserRegistration, out int SqlReturn, out string ReturnMessage)
        {

            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GGuid = new MySqlParameter("p_Guid", LoginUserGUID);
                SqlParamCollection.Add(GGuid);
                MySqlParameter GName = new MySqlParameter("p_Name", UserRegistration.Name);
                SqlParamCollection.Add(GName);
                MySqlParameter GPhoneNo = new MySqlParameter("p_PhoneNo", UserRegistration.PhoneNo);
                SqlParamCollection.Add(GPhoneNo);
                //MySqlParameter GImagePath = new MySqlParameter("p_ImagePath", UserRegistration.ImagePath);
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
                DS = DLComFunctions.ExecuteStoredProcedure("UpsertUserProfile", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                }
                else
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                SqlReturn = -1;
            }
        }

        public void ResetPasswords(string OldPassword, string NewPassword, string ConfirmPassword, out int SqlReturn, out string ReturnMessage)
        {
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GGuid = new MySqlParameter("p_Guid", LoginUserGUID);
                SqlParamCollection.Add(GGuid);
                MySqlParameter GOP = new MySqlParameter("p_OldPassword", OldPassword);
                SqlParamCollection.Add(GOP);
                MySqlParameter GNP = new MySqlParameter("p_NewPassword", NewPassword);
                SqlParamCollection.Add(GNP);
                MySqlParameter GCP = new MySqlParameter("p_ConfirmPassword", ConfirmPassword);
                SqlParamCollection.Add(GCP);
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
                DS = DLComFunctions.ExecuteStoredProcedure("ResetPasswords", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                }
                else
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                SqlReturn = -1;
            }
        }

        public string UserAuthorizedForThisOperation(string PageName, string RoleType, out int SqlReturn, out string ReturnMessage)
        {
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GGuid = new MySqlParameter("p_Guid", LoginUserGUID);
                SqlParamCollection.Add(GGuid);
                MySqlParameter GOP = new MySqlParameter("p_PageName", PageName);
                SqlParamCollection.Add(GOP);
                MySqlParameter GNP = new MySqlParameter("p_RoleType", RoleType);
                SqlParamCollection.Add(GNP);
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
                DS = DLComFunctions.ExecuteStoredProcedure("GetAuthorizationForLoggedInUser", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                    return ReturnMessage;
                }
                else
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                    return ReturnMessage;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                SqlReturn = -1;
                return ReturnMessage;
            }
        }

        public List<UserMenuEntities> GetUserMenu(out int SqlReturn, out string ReturnMessage)
        {
            List<UserMenuEntities> ListOfUM = new List<UserMenuEntities>();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GGuid = new MySqlParameter("p_LoginUserGUID", LoginUserGUID);
                SqlParamCollection.Add(GGuid);
              
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
                DS = DLComFunctions.ExecuteStoredProcedure("GetUserMenu", SqlParamCollection, ref RetResult, ref ErrCode);  
                if (ErrCode == 0)
                {
                    var Menus = DS.Tables[0];
                    ListOfUM = Menus.AsEnumerable()
                        .Select(row => new UserMenuEntities
                        {
                            PageGuid = row.Field<string>("PageGuid"),
                            RoleGUID = row.Field<string>("RoleGUID"),
                            PageName = row.Field<string>("PageName"),
                            RoleName = row.Field<string>("RoleName"),
                            RoleType = row.Field<string>("RoleType")
                        }).ToList();
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                    return ListOfUM;
                }
                else
                {
                    ReturnMessage = RetResult;
                    SqlReturn = ErrCode;
                    return ListOfUM;
                } 
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                SqlReturn = -1;
                return ListOfUM;
            }
        }
        
    }
}
