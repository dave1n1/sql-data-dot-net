using ArityInfowayBioMetric.Common;
using ArityInfowayBioMetric.Dal.Interface;
using ArityInfowayBioMetric.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArityInfowayBioMetric.Dal.Implemention
{
    public class UserService : IUserService
    {
        public Result<String> CheckLogin(string p_UserName, string p_Password)
        {
            Result<String> _Result = new Result<String>();
            try
            {
                _Result.IsSuccess = false;

                Guid _RoleId = new Guid(Helper.GetEnumDescription(Role.Administrator));

                string _Query = "select UserID from UserMaster where IsActive=1 and RoleId='" + _RoleId + "' and Username='" + p_UserName + "' and Password='" + p_Password + "'";

                SqlCommand _SqlCommand = new SqlCommand();
                _SqlCommand.CommandText = _Query;
                _SqlCommand.CommandType = CommandType.Text;

                string _LoginResult = SqlDataAccess.ExecuteScalar(_SqlCommand);

                if (!string.IsNullOrEmpty(_LoginResult))
                {
                    _Result.IsSuccess = true;
                    _Result.Data = _LoginResult;
                }
                else
                {
                    _Result.Message = Messages.FailLogin;
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
    }
}
