using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Earth
{
  public  class MagicDL
    {
        public class MySQLDLCommonFunctions
        {
            private readonly string connectionString;
            protected MySqlConnection _oConn;
            public string DBase_Connection_Str;
            private string m_Database;
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
                Database = "earth";
                Username = "root";
                Password = "12345";
                TimeOut = "360"; //ConfigurationSettings.AppSettings["TimeOut"]; 


                DBase_Connection_Str = "Server=" + Server + "; Port=3306; Database=" + Database + "; Uid=" + Username + "; Pwd=" + Password + "; default command timeout=0; Connection Timeout=10000;Convert Zero Datetime=True;";//

            }
            public DataSet ExecuteStoredProcedureTransactionModel(string SPName, List<MySqlParameter> Params, Boolean IsTransactionStarted, Boolean IsTransactionEnded, ref MySqlTransaction TransactionReference, ref string Result, ref int ReturnValue)
            {
                System.Data.DataSet DS = new System.Data.DataSet();

                MySqlCommand ocmd = null;
                MySqlTransaction myTrans = null;
                if (IsTransactionStarted)
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
                    if (ReturnValue == -1)
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
            //public void ExecuteNonQuery(string query)
            //{
            //    using (MySqlConnection connection = new MySqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        MySqlCommand command = new MySqlCommand(query, connection);
            //        command.ExecuteNonQuery();
            //    }
            //}
            public DataSet ExecuteStoredProcedure(string SPName, List<MySqlParameter> Params, ref string Result, ref int ReturnValue)
            {
                MySqlCommand ocmd = null;
                OpenConnection();
                ocmd = new MySqlCommand(SPName);

                ocmd.CommandType = CommandType.StoredProcedure;
                //ocmd.CommandType = System.Data.CommandType.Text;
                ocmd.Connection = _oConn;
                ocmd.CommandTimeout = 0; //infinite
                for (int i = 0;

                    i <= Params.Count - 1; i++)
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
            public MySqlDataReader ExecuteReader(string query)
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                return command.ExecuteReader();
            }


        }
    }
}
