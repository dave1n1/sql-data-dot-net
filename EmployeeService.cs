using ArityInfowayBioMetric.Dal.Interface;
using System;
using System.Collections.Generic;
using ArityInfowayBioMetric.Model;
using System.Data;
using System.Data.SqlClient;
using ArityInfowayBioMetric.Common;

namespace ArityInfowayBioMetric.Dal.Implemention
{
    public class EmployeeService : IEmployeeService
    {
        public Result<DataTable> GetAllEmployees()
        {
            Result<DataTable> _Result = new Result<DataTable>();
            try
            {
                _Result.IsSuccess = false;

                string _Query = @"select EmployeeID,REPLACE(FirstName + ' ' + MiddleName + ' ' + LastName, '  ', ' ') as FullName,convert(varchar, JoinDate, 103) JoinDate,
                                Email,MobileNo,ISNULL(is_having_fingureprint,0) as IsFinger,ISNULL(IsHavingFace,0) as IsFace,
                                (select count(EmployeeId) from EmployeeDeviceMap where IsActive=1 and EmployeeId = em.EmployeeID) DeviceCount from EmployeeMaster em where IsActive=1 and IsLeave=0";

                SqlCommand _SqlCommand = new SqlCommand();
                _SqlCommand.CommandText = _Query;
                _SqlCommand.CommandType = CommandType.Text;

                DataSet _DataSet = SqlDataAccess.RetriveDatabase(_SqlCommand, "EmployeeMaster");

                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    _Result.Data = _DataSet.Tables[0];
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<Employee>> GetAllSendPendingEmployeeByDevice(Guid p_DeviceId)
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();
            List<Employee> ListOfEmployee = new List<Employee>();
            Employee _Employee;

            try
            {
                _Result.IsSuccess = false;

                DataTable dataTable = new DataTable();

                string _Query = @"Select EmployeeID,REPLACE(FirstName + ' ' + MiddleName + ' ' + LastName, '  ', ' ') as FullName
                                from EmployeeMaster where IsActive= 1 and IsLeave = 0 and EmployeeID not in (Select EmployeeId from EmployeeDeviceMap where IsActive = 1 and DeviceId = @DeviceId)";

                SqlCommand _SqlCommand = new SqlCommand(_Query);

                _SqlCommand.Parameters.AddWithValue("@DeviceId", Convert.ToString(p_DeviceId));

                DataSet _DataSet = SqlDataAccess.RetriveDatabase(_SqlCommand, "Employee");

                if (_DataSet != null && _DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in _DataSet.Tables[0].Rows)
                    {
                        _Employee = new Employee();
                        _Employee.EmployeeID = new Guid(Convert.ToString(row["EmployeeID"]));
                        _Employee.FullName = Convert.ToString(row["FullName"]);
                        ListOfEmployee.Add(_Employee);
                        _Result.Data = ListOfEmployee;
                        _Result.IsSuccess = true;
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<bool> SaveEmployeeFingerPrint(Employee p_Employee)
        {
            Result<bool> _Result = new Result<bool>();
            try
            {
                _Result.IsSuccess = false;

                string _Query = @"update EmployeeMaster set FaceTemplateData=@FaceTemplateData, IsHavingFace=@IsHavingFace,FaceLength=@FaceLength,Password=@Password,is_having_fingureprint=@is_having_fingureprint,finger_template_data_tft=@finger_template_data_tft,
                                    finger_template_data_tft1=@finger_template_data_tft1,finger_template_data_tft2=@finger_template_data_tft2,finger_template_data_tft3=@finger_template_data_tft3,finger_template_data_tft4=@finger_template_data_tft4,finger_template_data_tft5=@finger_template_data_tft5,
                                    finger_template_data_tft6=@finger_template_data_tft6,finger_template_data_tft7=@finger_template_data_tft7,finger_template_data_tft8=@finger_template_data_tft8,finger_template_data_tft9=@finger_template_data_tft9 where EmployeeID=@EmployeeID";

                SqlCommand _SqlCommand = new SqlCommand();
                _SqlCommand.CommandText = _Query;
                _SqlCommand.CommandType = CommandType.Text;

                _SqlCommand.Parameters.AddWithValue("@FaceTemplateData", p_Employee.FaceTemplateData);
                _SqlCommand.Parameters.AddWithValue("@IsHavingFace", p_Employee.IsHavingFace);
                _SqlCommand.Parameters.AddWithValue("@FaceLength", p_Employee.FaceLength);
                _SqlCommand.Parameters.AddWithValue("@Password", p_Employee.Password);

                _SqlCommand.Parameters.AddWithValue("@is_having_fingureprint", p_Employee.is_having_fingureprint ?? false);

                if (p_Employee.finger_template_data_tft == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft", p_Employee.finger_template_data_tft);
                }

                if (p_Employee.finger_template_data_tft1 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft1", p_Employee.finger_template_data_tft1);
                }

                if (p_Employee.finger_template_data_tft2 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft2", p_Employee.finger_template_data_tft2);
                }

                if (p_Employee.finger_template_data_tft3 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft3", p_Employee.finger_template_data_tft3);
                }

                if (p_Employee.finger_template_data_tft4 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft4", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft4", p_Employee.finger_template_data_tft4);
                }

                if (p_Employee.finger_template_data_tft5 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft5", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft5", p_Employee.finger_template_data_tft5);
                }

                if (p_Employee.finger_template_data_tft6 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft6", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft6", p_Employee.finger_template_data_tft6);
                }

                if (p_Employee.finger_template_data_tft7 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft7", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft7", p_Employee.finger_template_data_tft7);
                }

                if (p_Employee.finger_template_data_tft8 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft8", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft8", p_Employee.finger_template_data_tft8);
                }

                if (p_Employee.finger_template_data_tft9 == null)
                {
                    _SqlCommand.Parameters.Add("@finger_template_data_tft9", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    _SqlCommand.Parameters.AddWithValue("@finger_template_data_tft9", p_Employee.finger_template_data_tft9);
                }

                _SqlCommand.Parameters.AddWithValue("@EmployeeID", p_Employee.EmployeeID);

                int _ResultOperation = SqlDataAccess.ExecuteOperation(_SqlCommand);

                if (_ResultOperation > 0)
                {
                    _Result.IsSuccess = true;
                    _Result.Data = true;
                }
                else
                {
                    _Result.Message = Messages.NoRecordMsg;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<DataTable> GetAllEmployeeFirstName()
        {
            Result<DataTable> _Result = new Result<DataTable>();
            try
            {
                _Result.IsSuccess = false;

                string _Query = "select distinct FirstName from EmployeeMaster where IsActive=1 and IsSend=1";

                SqlCommand _SqlCommand = new SqlCommand();
                _SqlCommand.CommandText = _Query;
                _SqlCommand.CommandType = CommandType.Text;

                DataSet _DataSet = SqlDataAccess.RetriveDatabase(_SqlCommand, "EmployeeMaster");

                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    _Result.Data = _DataSet.Tables[0];
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<DataTable> GetEmployeeAttendanceReportByEmpoyeeIdAndDate(string p_EmployeeName, DateTime p_FromDate, DateTime p_ToDate, Guid p_DeviceId)
        {
            Result<DataTable> _Result = new Result<DataTable>();
            try
            {
                _Result.IsSuccess = false;

                string _Query = @"select e.EmployeeID,REPLACE(e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName, '  ', ' ') as FullName, ea.AttendanceDate,ea.PunchTime,ea.AttendanceDateTime,ea.PunchMethod,
                                d.DeviceName,ROW_NUMBER() over (PARTITION BY e.EmployeeID,ea.AttendanceDate ORDER BY ea.AttendanceDateTime) AS SrNo from EmployeeMaster e inner join EmployeeAttendanceDevice ea on e.EmployeeID=ea.EmployeeId 
                                inner join DeviceMaster d on ea.DeviceID=d.DeviceID where e.IsActive=1 and ea.IsActive=1 and ea.AttendanceDate>=@FromDate and ea.AttendanceDate<=@ToDate";

                if (!string.IsNullOrEmpty(p_EmployeeName))
                {
                    _Query = _Query + " and e.FirstName like '%" + p_EmployeeName + "%'";
                }

                if (!string.IsNullOrEmpty(Convert.ToString(p_DeviceId)))
                {
                    _Query = _Query + " and d.DeviceID='" + p_DeviceId + "'";
                }

                SqlCommand _SqlCommand = new SqlCommand();
                _SqlCommand.CommandText = _Query;
                _SqlCommand.CommandType = CommandType.Text;

                _SqlCommand.Parameters.AddWithValue("@FromDate", Helper.DateToString(p_FromDate));
                _SqlCommand.Parameters.AddWithValue("@ToDate", Helper.DateToString(p_ToDate));

                DataSet _DataSet = SqlDataAccess.RetriveDatabase(_SqlCommand, "EmployeeAttendance");

                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    _Result.Data = _DataSet.Tables[0];
                    _Result.IsSuccess = true;
                }
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }

        public Result<List<Employee>> GetAllEmployeeWithEnrollNo()
        {
            Result<List<Employee>> _Result = new Result<List<Employee>>();
            List<Employee> _ListOfEmployee = new List<Employee>();
            Employee _Employee;

            try
            {
                _Result.IsSuccess = false;

                string _Query = @"Select em.EmployeeID,FirstName,LastName,Email,EnrollNo from EmployeeMaster em inner join  
                                    EmployeeDeviceMap edm on em.EmployeeID = edm.EmployeeId 
                                    where em.IsActive = 1 and edm.IsActive = 1 and em.IsLeave = 0";

                SqlCommand _SqlCommand = new SqlCommand();

                _SqlCommand.CommandText = _Query;

                DataSet _DataSet = SqlDataAccess.RetriveDatabase(_SqlCommand, "Employee");

                if (_DataSet != null)
                {
                    if (_DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow emp in _DataSet.Tables[0].Rows)
                        {
                            _Employee = new Employee();
                            _Employee.EmployeeID = new Guid(emp["EmployeeID"].ToString());
                            _Employee.FirstName = emp["FirstName"].ToString();
                            _Employee.LastName = emp["LastName"].ToString();
                            _Employee.Email = emp["Email"].ToString();
                            _Employee.EnrollNo = emp["EnrollNo"].ToString();
                            _ListOfEmployee.Add(_Employee);
                            _Result.Data = _ListOfEmployee;
                            _Result.IsSuccess = true;
                        }
                    }
                }

                _Result.Message = Messages.NoRecordMsg;
            }
            catch (Exception _Exception)
            {
                _Result.IsSuccess = false;
                _Result.Message = Messages.ExceptionMsg;
                _Result.Exception = _Exception;
            }
            return _Result;
        }
    }
}
