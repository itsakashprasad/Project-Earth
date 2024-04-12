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
    public class PageRoleMappingBL
    {
        public PageRoleMappingEntities GetpagerolesMapping(string PageGuid, out PageRoleMappingEntities AdditionalColumn, out int ReturnValue, out string ReturnMessage)
        {
            PageRoleMappingEntities objAdditional = new PageRoleMappingEntities();
            PageRoleMappingEntities objppspagerolesMapping = new PageRoleMappingEntities();
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GD = new MySqlParameter("p_PageGuid", PageGuid);
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
                DS = DLComFunctions.ExecuteStoredProcedure("GetpagerolesMapping", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var AssignRole = DS.Tables[0];
                    var NotAssignRole = DS.Tables[1];
                    objppspagerolesMapping.AssignRole = AssignRole.AsEnumerable()
                    .Select(row => new MappingRole
                    {
                        Guid = row.Field<string>("Guid"),
                        Name = row.Field<String>("Name"),
                    }).ToList();
                    objppspagerolesMapping.NotAssignRole = NotAssignRole.AsEnumerable()
                    .Select(row => new MappingRole
                    {
                        Guid = row.Field<string>("Guid"),
                        Name = row.Field<String>("Name"),
                    }).ToList();
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    AdditionalColumn = objAdditional;
                    return objppspagerolesMapping;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    AdditionalColumn = objAdditional;
                    return objppspagerolesMapping;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                AdditionalColumn = objAdditional;
                return objppspagerolesMapping;
            }
        }
        public void UpsertpagerolesMapping(List<MappingRole> ListOfRole, PageRoleMappingEntities objParam, out int ReturnValue, out string ReturnMessage)
        {
            try
            {
                int ErrCode = -1;
                DataSet DS = new DataSet();
                var SqlParamCollection = new List<MySqlParameter>();
                MySqlParameter GPageGuid = new MySqlParameter("p_PageGuid", objParam.PageGuid);
                SqlParamCollection.Add(GPageGuid);
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
                MySqlTransaction TransactionReference = null;
                Boolean IsTransactionEnded = false;
                if (ListOfRole.Count == 0)
                {
                    IsTransactionEnded = true;
                }
                DS = DLComFunctions.ExecuteStoredProcedureTransactionModel("DeletepagerolesMapping", SqlParamCollection, true, IsTransactionEnded, ref TransactionReference, ref RetResult, ref ErrCode);
                if (ErrCode == -1)
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                }
                else
                {
                    int TransactionDetailsCount = ListOfRole.Count;
                    int CurrentTransactionDetail = 0;
                    foreach (MappingRole objRole in ListOfRole)
                    {
                        CurrentTransactionDetail++;
                        var SqlParamCollectionDetails = new List<MySqlParameter>();
                        MySqlParameter p_PageGuid = new MySqlParameter("p_PageGuid", objParam.PageGuid);
                        SqlParamCollectionDetails.Add(p_PageGuid);
                        MySqlParameter p_Guid = new MySqlParameter("p_Guid", objRole.Guid);
                        SqlParamCollectionDetails.Add(p_Guid);
                        string RetResultDetails = "";
                        MySqlParameter ResultDetails = new MySqlParameter("p_Result", typeof(string));
                        ResultDetails.Value = RetResultDetails;
                        ResultDetails.Size = 4000;
                        ResultDetails.Direction = System.Data.ParameterDirection.Output;
                        SqlParamCollectionDetails.Add(ResultDetails);
                        int ErrorCodeDetails = -1;
                        MySqlParameter RetIntDetails = new MySqlParameter("p_Return", typeof(Int32));
                        RetIntDetails.Value = ErrorCodeDetails;
                        RetIntDetails.Direction = System.Data.ParameterDirection.Output;
                        SqlParamCollectionDetails.Add(RetIntDetails);
                        MySQLDLCommonFunctions DLComFunctionsDetails = new MySQLDLCommonFunctions();
                        DataSet DSDetails = DLComFunctionsDetails.ExecuteStoredProcedureTransactionModel("UpsertpagerolesMapping", SqlParamCollectionDetails, false, (CurrentTransactionDetail == TransactionDetailsCount ? true : false), ref TransactionReference, ref RetResultDetails, ref ErrorCodeDetails);
                        ErrCode = ErrorCodeDetails;
                        RetResult = RetResultDetails;
                        if (ErrCode == -1)
                        {
                            ReturnMessage = RetResultDetails;
                            ReturnValue = ErrorCodeDetails;
                        }
                    }
                    ReturnMessage = "Successfully mapping done";
                    ReturnValue = 0;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
            }
        }
        public List<MappingPage> GetpagesForMapping(out int ReturnValue, out string ReturnMessage)
        {
            List<MappingPage> ListOfPage = new List<MappingPage>();
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
                DS = DLComFunctions.ExecuteStoredProcedure("GetpagesForMapping", SqlParamCollection, ref RetResult, ref ErrCode);
                if (ErrCode == 0)
                {
                    var Page = DS.Tables[0];
                    ListOfPage = Page.AsEnumerable()
                    .Select(row => new MappingPage
                    {
                        Guid = row.Field<string>("Guid"),
                        Name = row.Field<String>("Name"),
                    }).ToList();
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfPage;
                }
                else
                {
                    ReturnMessage = RetResult;
                    ReturnValue = ErrCode;
                    return ListOfPage;
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                ReturnValue = -1;
                return ListOfPage;
            }
        }
    }
}
