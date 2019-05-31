using ArityInfowayBioMetric.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArityInfowayBioMetric.Dal
{
    public static class SqlDataAccess
    {
        static string _ConnectionString = ConfigurationManager.ConnectionStrings["ArityInfowayBioMetricConnectionString"].ToString();

        public static DataSet RetriveDatabase(SqlCommand p_SqlCommand, string p_TableName)
        {
            SqlConnection _SqlConnection = new SqlConnection();
            int _AttemptedTry = 0;
            DataSet _DataSet = null;

            try
            {
                _SqlConnection.ConnectionString = _ConnectionString;
                p_SqlCommand.Connection = _SqlConnection;
            // open connection
            tryagain:
                try
                {
                    _SqlConnection.Open();
                }
                catch (Exception _Exception)
                {
                    Helper.LogFile(_Exception.Message);

                    #region EXTRA
                    if (_Exception.Message.ToLower().Contains("timeout"))
                    {
                        if (_AttemptedTry < 3)
                        {
                            System.Threading.Thread.Sleep(1000);
                            _AttemptedTry++;
                            goto tryagain;
                        }
                    }
                    #endregion
                }

                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter(p_SqlCommand);
                _DataSet = new DataSet();

                if (p_TableName != "")
                {
                    _SqlDataAdapter.Fill(_DataSet, p_TableName);
                }
                else
                {
                    _SqlDataAdapter.Fill(_DataSet);
                }

                //Close connection.
                if (_SqlConnection.State == ConnectionState.Open)
                {
                    _SqlConnection.Close();
                }
                //  return ds;
            }
            catch (Exception _Exception)
            {
                Helper.LogFile(_Exception.Message);
            }
            finally
            {
                if (_SqlConnection != null)
                {
                    if (_SqlConnection.State == ConnectionState.Open)
                    {
                        try
                        {
                            _SqlConnection.Close();
                        }
                        catch (Exception _Exception)
                        {
                            Helper.LogFile(_Exception.Message);
                        }
                    }
                }
            }
            return _DataSet;
        }

        public static int ExecuteOperation(SqlCommand p_SqlCommand)
        {
            int _Result = 0;
            int _AttemptedTry = 0;
            SqlConnection _SqlConnection = new SqlConnection(_ConnectionString);
            p_SqlCommand.Connection = _SqlConnection;
            p_SqlCommand.CommandTimeout = 0;
            try
            {
                if (_SqlConnection.State == ConnectionState.Closed)
                {
                tryagain:
                    try
                    {
                        _SqlConnection.Open();
                    }
                    catch (Exception _Exception)
                    {
                        Helper.LogFile(_Exception.Message);
                        #region EXTRA
                        if (_Exception.Message.ToLower().Contains("timeout"))
                        {
                            if (_AttemptedTry < 3)
                            {
                                System.Threading.Thread.Sleep(1000);
                                _AttemptedTry++;
                                goto tryagain;
                            }
                        }
                        #endregion
                    }
                    _Result = p_SqlCommand.ExecuteNonQuery();
                }
                else if (_SqlConnection.State == ConnectionState.Open)
                {
                    _Result = p_SqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception _Exception)
            {
                Helper.LogFile(_Exception.Message);
                p_SqlCommand.Dispose();
                throw _Exception;
            }
            finally
            {
                p_SqlCommand.Dispose();
                if (_SqlConnection != null)
                {
                    if (_SqlConnection.State == ConnectionState.Open)
                    {
                        try
                        {
                            _SqlConnection.Close();
                        }
                        catch (Exception _Exception)
                        {
                            Helper.LogFile(_Exception.Message);
                        }
                    }
                }
            }
            return _Result;
        }

        public static String ExecuteScalar(SqlCommand p_SqlCommand)
        {
            String _Result = string.Empty;
            int _AttemptedTry = 0;
            SqlConnection _SqlConnection = new SqlConnection(_ConnectionString);
            p_SqlCommand.Connection = _SqlConnection;
            p_SqlCommand.CommandTimeout = 0;
            try
            {
                if (_SqlConnection.State == ConnectionState.Closed)
                {
                tryagain:
                    try
                    {
                        _SqlConnection.Open();
                    }
                    catch (Exception _Exception)
                    {
                        Helper.LogFile(_Exception.Message);

                        #region EXTRA
                        if (_Exception.Message.ToLower().Contains("timeout"))
                        {
                            if (_AttemptedTry < 3)
                            {
                                System.Threading.Thread.Sleep(1000);
                                _AttemptedTry++;
                                goto tryagain;
                            }
                        }
                        #endregion
                    }
                    _Result = Convert.ToString(p_SqlCommand.ExecuteScalar());
                }
                else if (_SqlConnection.State == ConnectionState.Open)
                {
                    _Result = Convert.ToString(p_SqlCommand.ExecuteScalar());
                }
            }
            catch (Exception _Exception)
            {
                Helper.LogFile(_Exception.Message);
                p_SqlCommand.Dispose();
                throw _Exception;
            }
            finally
            {
                p_SqlCommand.Dispose();
                if (_SqlConnection != null)
                {
                    if (_SqlConnection.State == ConnectionState.Open)
                    {
                        try
                        {
                            _SqlConnection.Close();
                        }
                        catch (Exception _Exception)
                        {
                            Helper.LogFile(_Exception.Message);
                        }
                    }
                }
            }
            return _Result;
        }

    }
}
