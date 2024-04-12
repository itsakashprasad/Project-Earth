using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Globalization;
using System.Net;

namespace Earth
{
    public class MySQLDLCommonFunctions
    {
        private string m_Database;
        public string DBase_Connection_Str;

        protected MySqlConnection _oConn;
        public string Database
        {
            get { return m_Database; }
            set { m_Database = value; }
        }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Server { set; get; }
       
        public string TimeOut { set; get; }

        public MySQLDLCommonFunctions()
        {
			//DEVELOPER SETTINGS

			Server = "localhost";// ConfigurationSettings.AppSettings["DBServer"];
			Database = "printing_setup";
			Username = "root";
			Password = "sa#0";
			TimeOut = "360"; //ConfigurationSettings.AppSettings["TimeOut"]; 

			DBase_Connection_Str = "Server="+ Server + "; Port=3306; Database="+Database+"; Uid="+Username+"; Pwd="+Password+ "; default command timeout=0; Connection Timeout=10000;Convert Zero Datetime=True;SslMode=None;";//

        }
        public void OpenConnection()
        {

            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                if (_oConn == null)
                {
                    _oConn = new MySqlConnection(DBase_Connection_Str);
                    _oConn.Open();
                }
                else if (_oConn.State != ConnectionState.Open)
                {
                    _oConn.ConnectionString = DBase_Connection_Str;
                    _oConn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "::" + System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name + "::" + ex.Message);
            }
        }
        public void CloseConnection()
        {
            try
            {
                if (_oConn.State != ConnectionState.Closed)
                {
                    _oConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "::" + System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name + "::" + System.Reflection.MethodInfo.GetCurrentMethod().Name + "::" + ex.Message);
            }
        }
        public DataSet GetQueryResults(string Query, ref string ErrorMsg)
        {
            MySqlConnection RS_Connect = new MySqlConnection();
            try
            {
                RS_Connect.ConnectionString = DBase_Connection_Str;
                
            }
            
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                ErrorMsg = ex.Message;
                return null;
            }

            MySqlDataAdapter Data_Adapter = new MySqlDataAdapter(Query, RS_Connect);
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                Data_Adapter.Fill(DS);
            }
            catch (MySqlException ex)
            {
                ErrorMsg = ex.Message;
                return null;
            }
            Data_Adapter = null;
            RS_Connect.Close();
            return DS;

        }
        public DataSet ExecuteStoredProcedure(string SPName, List<MySqlParameter> Params, ref string Result, ref int ReturnValue)
        {
            MySqlCommand ocmd = null;
            OpenConnection();
            ocmd = new MySqlCommand(SPName);

            ocmd.CommandType = CommandType.StoredProcedure;
            //ocmd.CommandType = System.Data.CommandType.Text;
            ocmd.Connection = _oConn;
            ocmd.CommandTimeout = 0; //infinite
            for (int i = 0; i <= Params.Count - 1; i++)
            {
                MySqlParameter Param = new MySqlParameter();
                var _with1 = Param;
                _with1.ParameterName = Params[i].ParameterName;
                _with1.Value = Params[i].Value;
                _with1.Direction = Params[i].Direction;
                _with1.Size = Params[i].Size;
                ocmd.Parameters.Add(Param);
            }

            MySqlDataAdapter Data_Adapter = new MySqlDataAdapter(ocmd);
            System.Data.DataSet DS = new System.Data.DataSet();
            try
            {
                Data_Adapter.Fill(DS);
                Result = ocmd.Parameters["p_Result"].Value.ToString();
                ReturnValue = Convert.ToInt32(ocmd.Parameters["p_Return"].Value.ToString());
                CloseConnection();
            }
            catch (MySqlException ex)
            {
                Result = ex.Message;
                //ReturnValue = 0;
                //CloseConnection();
                //return null;
                throw;
            }
            Data_Adapter = null;
            return DS;
        }

        public DataSet ExecuteStoredProcedureTransactionModel(string SPName, List<MySqlParameter> Params, Boolean IsTransactionStarted, Boolean IsTransactionEnded, ref MySqlTransaction TransactionReference, ref string Result, ref int ReturnValue)
        {
            System.Data.DataSet DS = new System.Data.DataSet();

            MySqlCommand ocmd = null;
            MySqlTransaction myTrans = null;
            if(IsTransactionStarted)
            {
                OpenConnection();
                myTrans = _oConn.BeginTransaction();
                TransactionReference = myTrans;
            }

            ocmd = new MySqlCommand(SPName);
            ocmd.Transaction = TransactionReference;
            ocmd.CommandType = CommandType.StoredProcedure;
            //Set Reference of Connection to Tranasction Reference
            _oConn = TransactionReference.Connection;
            ocmd.Connection = _oConn;
            

            ocmd.CommandTimeout = 0; //infinite
            for (int i = 0; i <= Params.Count - 1; i++)
            {
                MySqlParameter Param = new MySqlParameter();
                var _with1 = Param;
                _with1.ParameterName = Params[i].ParameterName;
                _with1.Value = Params[i].Value;
                _with1.Direction = Params[i].Direction;
                _with1.Size = Params[i].Size;
                ocmd.Parameters.Add(Param);
            }

            MySqlDataAdapter Data_Adapter = new MySqlDataAdapter(ocmd);
                
            try
            {
                Data_Adapter.Fill(DS);
                Result = ocmd.Parameters["p_Result"].Value.ToString();
                ReturnValue = Convert.ToInt32(ocmd.Parameters["p_Return"].Value.ToString());
                if(ReturnValue == -1)
                {
                    
                    TransactionReference.Rollback();
                    CloseConnection();
                    
                }
                if (IsTransactionEnded)
                {
                    TransactionReference.Commit();
                    CloseConnection();
                }
                   
            }
            catch (MySqlException ex)
            {
                Result = ex.Message;
                TransactionReference.Rollback();
                //ReturnValue = 0;
                //CloseConnection();
                //return null;
                throw;
            }
            Data_Adapter = null;
                
            
            return DS;
        }

        /// <summary>
        /// Bulk insert some data, uses parameters
        /// </summary>
        /// <param name="table">The Table Name</param>
        /// <param name="inserts">Holds list of data to insert</param>
        /// <param name="batchSize">executes the insert after batch lines</param>
        /// <param name="progress">Progress reporting</param>
        public void BulkInsert(string table, MySQLBulkInsertData inserts, int batchSize = 100, IProgress<double> progress = null)
        {
            if (inserts.Count <= 0) throw new ArgumentException("Nothing to Insert");

            string insertcmd = string.Format("INSERT INTO `{0}` ({1}) VALUES ", table,
                                             inserts.Fields.Select(p => p.FieldName).ToCSV());
            StringBuilder sb = new StringBuilder();
            using (MySqlConnection conn = new MySqlConnection(DBase_Connection_Str))
            using (MySqlCommand sqlExecCommand = conn.CreateCommand())
            {
                conn.Open();
                sb.AppendLine(insertcmd);
                for (int i = 0; i < inserts.Count; i++)
                {
                    sb.AppendLine(ToParameterCSV(inserts.Fields, i));
                    for (int j = 0; j < inserts[i].Count(); j++)
                    {
                        sqlExecCommand.Parameters.AddWithValue(string.Format("{0}{1}", inserts.Fields[j].FieldName, i), inserts[i][j]);
                    }
                    //commit if we are on the batch sizeor the last item
                    if (i > 0 && (i % batchSize == 0 || i == inserts.Count - 1))
                    {
                        sb.Append(";");
                        sqlExecCommand.CommandText = sb.ToString();
                        sqlExecCommand.ExecuteNonQuery();
                        //reset the stringBuilder
                        sb.Clear();
                        sb.AppendLine(insertcmd);
                        if (progress != null)
                        {
                            progress.Report((double)i / inserts.Count);
                        }
                    }
                    else
                    {
                        sb.Append(",");
                    }
                }
            }
        }

        private string ToParameterCSV(IEnumerable<MySQLBulkInsertData.MySQLFieldDefinition> p, int row)
        {
            string csv = p.Aggregate(string.Empty,
                (current, i) => string.IsNullOrEmpty(current)
                        ? string.Format("@{0}{1}", i.FieldName, row)
                        : string.Format("{0},@{2}{1}", current, row, i.FieldName));
            return string.Format("({0})", csv);
        }


        /// <summary>
        /// Bulk Import Invoice Details into Databas
        /// </summary>
        /// <param name="list"></param>
        public void BulkImportInvoiceDetails(DataTable dtData)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO pos_stock_details (GUID, DistrictGUID,LocationGUID,DepartmentGUID,StallGUID,InvoiceNo,InvoiceDate,ItemCode,OpeningQuantity,OpeningAmount,UnitPrice,AddDate,AddByGUID) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(DBase_Connection_Str))
            {
                List<string> Rows = new List<string>();

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    Guid GUID = Guid.NewGuid();
                    string DistrictGUID = Convert.ToString(dtData.Rows[i]["DistrictGUID"].ToString());
                    string LocationGUID = Convert.ToString(dtData.Rows[i]["LocationGUID"].ToString());
                    string DepartmentGUID = Convert.ToString(dtData.Rows[i]["DepartmentGUID"].ToString());
                    string StallGUID = Convert.ToString(dtData.Rows[i]["StallGUID"].ToString());
                    string InvoiceNo = dtData.Rows[i]["InvoiceNo"].ToString();

                    string ConvertedInvoiceDate= Convert.ToDateTime(dtData.Rows[i]["InvoiceDate"].ToString()).ToString("yyyy/MM/dd");
                    //DateTime InvoiceDate =  //Convert.ToDateTime(ConvertedInvoiceDate);

                    string ItemCode = dtData.Rows[i]["ItemCode"].ToString();
                    string OpeningQuantity = dtData.Rows[i]["OpeningQuantity"].ToString();
                    string OpeningAmount = dtData.Rows[i]["OpeningAmount"].ToString();
                    Decimal UnitPrice = Convert.ToDecimal(dtData.Rows[i]["UnitPrice"].ToString());
                    string AddByGUID = dtData.Rows[i]["AddByGUID"].ToString(); ;


                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                        GUID, DistrictGUID, LocationGUID, DepartmentGUID, StallGUID, InvoiceNo, ConvertedInvoiceDate,
                        ItemCode, OpeningQuantity, OpeningAmount, UnitPrice, DateTime.Now.ToString("yyyy/MM/dd"), AddByGUID));
                }

                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");
                mConnection.Open();
                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    myCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
