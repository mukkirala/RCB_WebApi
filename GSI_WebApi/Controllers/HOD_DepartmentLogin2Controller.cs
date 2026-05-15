using NRL_WebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class HOD_DepartmentLogin2Controller : ApiController
    {
        [HttpPost]
        public List<HOD_DepartmentLogin> checkHODAuthorization(HOD_DepartmentLogin data)
        {
            List<HOD_DepartmentLogin> GetDepartment = new List<HOD_DepartmentLogin>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    string value = data.USR_PASSWORD;
                    string passed = Utilities.EncryptTripleDES(value);
                   // string sqlState = "SELECT  Department FROM tblUSER WHERE USR_LOGIN='" + data.USR_LOGIN + "' AND USR_PASSWORD = '" + passed + "'";
                    cmd.CommandText = "SELECT  Department, SecondDepartment FROM tblUSER WHERE USR_LOGIN='" + data.USR_LOGIN + "' AND USR_PASSWORD = '" + passed + "'";
                    //"SELECT  AppointmentID, Name, PurposeofVisit, MobileNo, HostName FROM AppointmentSchedule WHERE DepartmentID='" + data.Department+ "' AND Status='Pending'";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        HOD_DepartmentLogin GetDept = new HOD_DepartmentLogin();
                        GetDept.Department = rdr["Department"].ToString();
                        GetDept.SecondDepartment = rdr["SecondDepartment"].ToString();
                        GetDepartment.Add(GetDept);
                    }
                    conn.Close();
                    return GetDepartment;
                }
            }
        }
    }
}
